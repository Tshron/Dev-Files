import numpy as np
import itertools
np.random.seed(42)

def preprocess(X, y):
    """
    Perform mean normalization on the features and divide the true labels by
    the range of the column. 

    Input:
    - X: Inputs  (n features over m instances).
    - y: True labels.

    Returns a two vales:
    - X: The mean normalized inputs.
    - y: The scaled labels.
    """
    # Get average of each row
    row_avg_x = X.mean(axis=0)
    X = X - row_avg_x

    # Compute min of each feature
    min_col = X.min(axis=0)
    max_col = X.max(axis=0)
    res_col = max_col - min_col
    X = X / res_col

    # mean normalisation for y
    y = (y - y.mean()) / (y.max() - y.min())
    return X, y

def compute_cost(X, y, theta):
    """
    Computes the average squared difference between an observation’s actual and
    predicted values for linear regression.  

    Input:
    - X: Inputs  (n features over m instances).
    - y: True labels (1 value over m instances).
    - theta: The parameters (weights) of the model being learned.

    Returns a single value:
    - J: the cost associated with the current set of parameters (single number).
    """

    m = np.size(X, 0)
    predict = np.matmul(X, theta) # calculates the prediction by matrix multiplication of the thetas and the features
    error = predict - y # calculates the distance between the prediction and the truth values
    J = np.matmul(error, error) # multiplication of two vectors performs power on each element and sum is all together
    J = J / (2*m) # calc the average by divide J by the number of instances in X

    return J

def gradient_descent(X, y, theta, alpha, num_iters):
    """
    Learn the parameters of the model using gradient descent. Gradient descent
    is an optimization algorithm used to minimize some (loss) function by 
    iteratively moving in the direction of steepest descent as defined by the
    negative of the gradient. We use gradient descent to update the parameters
    (weights) of our model.

    Input:
    - X: Inputs  (n features over m instances).
    - y: True labels (1 value over m instances).
    - theta: The parameters (weights) of the model being learned.
    - alpha: The learning rate of your model.
    - num_iters: The number of updates performed.

    Returns two values:
    - theta: The learned parameters of your model.
    - J_history: the loss value for every iteration.
    """
    
    J_history = [] # Use a python list to save cost in every iteration
    for i in range(num_iters):
        # calculates the prediction by matrix multiplication of the thetas and the features
        predict = np.dot(X, theta)
        error = predict - y  # calculates the distance between the prediction and the truth values
        # multiplication of the two vectors performs multiplication of the current Xj with the distance array
        sigma = np.matmul(X.T,error)
        theta = np.subtract(theta,((alpha/np.size(X, 0)) * sigma))  # subtract it all from the previous theta

        # calculations for the new cost function
        J_history.append(compute_cost(X,y,theta))

    return theta, J_history

def pinv(X, y):
    """
    Calculate the optimal values of the parameters using the pseudoinverse
    approach as you saw in class.

    Input:
    - X: Inputs  (n features over m instances).
    - y: True labels (1 value over m instances).

    Returns two values:
    - theta: The optimal parameters of your model.

    ########## DO NOT USE numpy.pinv ##############
    #"""
    transpose = np.transpose(X)
    dotPrud1 = np.matmul(transpose,X)
    inverse = np.linalg.inv(dotPrud1)
    dotPrud = np.matmul(inverse, transpose)
    pinv_theta = np.matmul(dotPrud, y)

    return pinv_theta

def efficient_gradient_descent(X, y, theta, alpha, num_iters):
    """
    Learn the parameters of your model, but stop the learning process once
    the improvement of the loss value is smaller than 1e-8. This function is
    very similar to the gradient descent function you already implemented.

    Input:
    - X: Inputs  (n features over m instances).
    - y: True labels (1 value over m instances).
    - theta: The parameters (weights) of the model being learned.
    - alpha: The learning rate of your model.
    - num_iters: The number of updates performed.

    Returns two values:
    - theta: The learned parameters of your model.
    - J_history: the loss value for every iteration.
    """

    J_history = []  # Use a python list to save cost in every iteration
    for i in range(num_iters):
        # calculates the prediction by matrix multiplication of the thetas and the features
        predict = np.matmul(X, theta)
        error = predict - y  # calculates the distance between the prediction and the truth values
        # multiplication of the two vectors performs multiplication of the current Xj with the distance array
        sigma = np.matmul(error, X)
        theta = np.subtract(theta,((alpha / np.size(X, 0)) * sigma))  # subtract it all from the previous theta

        # calculations for the new cost function
        J_history.append(compute_cost(X, y, theta))

        if len(J_history) >= 2 and (J_history[-2] - J_history[-1]) < 1e-8:
            break


    return theta, J_history

def find_best_alpha(X, y, iterations):
    """
    Iterate over provided values of alpha and maintain a python dictionary 
    with alpha as the key and the final loss as the value.

    Input:
    - X: a dataframe that contains all relevant features.

    Returns:
    - alpha_dict: A python dictionary that hold the loss value after training 
    for every value of alpha.
    """
    
    alphas = [0.00001, 0.00003, 0.0001, 0.0003, 0.001, 0.003, 0.01, 0.03, 0.1, 0.3, 1, 2, 3]
    alpha_dict = {}
    features = np.size(X,1)
    theta = np.ones(features)
    for alpha in alphas:
        J_history = gradient_descent(X,y,theta,alpha,iterations)[1]
        alpha_dict[alpha] = J_history[-1]

    return alpha_dict

def generate_triplets(X):
    """
    generate all possible sets of three features out of all relevant features
    available from the given dataset X. You might want to use the itertools
    python library.

    Input:
    - X: a dataframe that contains all relevant features.

    Returns:
    - A python list containing all feature triplets.
    """
    triplets = []
    tuples = itertools.combinations (X, 3)
    for tup in tuples:
        triplets.append(tup)

    return triplets

def find_best_triplet(df, triplets, alpha, num_iter):
    """
    Iterate over all possible triplets and find the triplet that best 
    minimizes the cost function. For better performance, you should use the 
    efficient implementation of gradient descent. You should first preprocess
    the data and obtain a array containing the columns corresponding to the
    triplet. Don't forget the bias trick.

    Input:
    - df: A dataframe that contains the data
    - triplets: a list of three strings representing three features in X.
    - alpha: The value of the best alpha previously found.
    - num_iters: The number of updates performed.

    Returns:
    - The best triplet as a python list holding the best triplet as strings.
    """
    best_triplet = None
    currentBestTripletCost = 99999 # initialized at big amount for sureness

    for triplet in triplets:
        # preprocessing the corresponding columns from the dataset into arrays
        columns = [triplet[0],triplet[1],triplet[2]]
        X = df[columns]
        y = df['price']
        X = np.array(X)
        y = np.array(y)
        X, y = preprocess(X,y)

        # the bias trick
        Y = np.ones(np.size(X, 0))
        X = np.column_stack((Y, X))

        theta = np.ones(np.size(X, 1))

        tmp_theta, J_history = efficient_gradient_descent(X,y,theta,alpha,num_iter)
        # if the current triplet minimizes the cost function better, set it as the best triplet
        if J_history[-1] < currentBestTripletCost:
            currentBestTripletCost = J_history[-1]
            best_triplet = triplet

    return best_triplet
