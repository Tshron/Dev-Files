import numpy as np
np.random.seed(42)

####################################################################################################
#                                            Part A
####################################################################################################

class NaiveNormalClassDistribution():
    def __init__(self, dataset, class_value):
        """
        A class which encapsulate the relevant parameters(mean, std) for a class conditinoal normal distribution.
        The mean and std are computed from a given data set.
        
        Input
        - dataset: The dataset from which to compute the mean and mu (Numpy Array).
        - class_value : The class to calculate the mean and mu for.
        """
        self.data = dataset
        self.class_value = class_value
        ##suadata = subadata[:, 0:2]
        self.mean = np.mean(dataset, axis=0)
        self.std = np.std(dataset, axis=0)


    
    def get_prior(self):
        """
        Returns the prior porbability of the class according to the dataset distribution.
        """
        classInstances = self.data[self.data[:, -1] == self.class_value]
        return len(classInstances) / len(self.data)
    
    def get_instance_likelihood(self, x):
        """
        Returns the likelihhod porbability of the instance under the class according to the dataset distribution.
        """
        numberOfFeatures = np.size(self.data, 1) - 1
        likelihoodProb = 1
        for i in range(numberOfFeatures):
            likelihoodProb *= normal_pdf(x[i], self.mean[i], self.std[i])
        return likelihoodProb

    def get_instance_posterior(self, x):
        """
        Returns the posterior porbability of the instance under the class according to the dataset distribution.
        * Ignoring p(x)
        """

        return (self.get_instance_likelihood(x) * self.get_prior())
    
class MultiNormalClassDistribution():
    def __init__(self, dataset, class_value):
        """
        A class which encapsulate the relevant parameters(mean, cov matrix) for a class conditinoal multi normal distribution.
        The mean and cov matrix (You can use np.cov for this!) will be computed from a given data set.
        
        Input
        - dataset: The dataset from which to compute the mean and mu (Numpy Array).
        - class_value : The class to calculate the mean and mu for.
        """
        self.data = dataset
        self.class_value = class_value
        subadata = dataset[dataset[:, -1] == class_value]
        datasetForMean = subadata[:,0:2]
        self.mean = np.mean(datasetForMean,axis=0, keepdims=True)
        self.cov = np.cov(np.transpose(datasetForMean))

        
        
    def get_prior(self):
        """
        Returns the prior porbability of the class according to the dataset distribution.
        """
        length = len(self.data)
        classInstances = self.data[self.data[:, -1] == self.class_value]
        val ,number = np.unique(self.data, return_counts=True)
        index = np.where(val == self.class_value)

        return len(number[index]) / length
    
    def get_instance_likelihood(self, x):
        """
        Returns the likelihhod porbability of the instance under the class according to the dataset distribution.
        """

        likelihoodProb = multi_normal_pdf(x, self.mean, self.cov)
        return likelihoodProb


    def get_instance_posterior(self, x):
        """
        Returns the posterior porbability of the instance under the class according to the dataset distribution.
        * Ignoring p(x)
        """
        return (self.get_instance_likelihood(x)* self.get_prior())
    
    

def normal_pdf(x, mean, std):
    """
    Calculate normal desnity function for a given x, mean and standrad deviation.
 
    Input:
    - x: A value we want to compute the distribution for.
    - mean: The mean value of the distribution.
    - std:  The standard deviation of the distribution.
 
    Returns the normal distribution pdf according to the given mean and var for the given x.    
"""
    a = 1/(np.sqrt(2 * np.pi * std*std))
    b = np.abs(np.power(x - mean, 2))
    c = np.exp(-(b) / (2 * std))
    return a * c

    
def multi_normal_pdf(x, mean, cov):
    """
    Calculate multi variante normal desnity function for a given x, mean and covarince matrix.
 
    Input:
    - x: A value we want to compute the distribution for.
    - mean: The mean value of the distribution.
    - std:  The standard deviation of the distribution.
 
    Returns the normal distribution pdf according to the given mean and var for the given x.    
    """

    x = x[:-1]
    aa = (2 * np.pi) ** (-(len(x) / 2))

    ab = np.linalg.det(cov) ** (-0.5)
    a = aa * ab

    expA = np.dot(-(1/2) ,np.transpose(x - mean))
    expB = np.dot(np.linalg.inv(cov),expA)
    expC = np.matmul((x - mean), expB)
    b = np.exp(expC)
    return np.dot(a,b)

####################################################################################################
#                                            Part B
####################################################################################################
EPSILLON = 1e-6 # == 0.000001 It could happen that a certain value will only occur in the test set.
                # In case such a thing occur the probability for that value will EPSILLON.

class DiscreteNBClassDistribution():
    def __init__(self, dataset, class_value):
        """
        A class which computes and encapsulate the relevant probabilites for a discrete naive bayes 
        distribution for a specific class. The probabilites are computed with la place smoothing.
        
        Input
        - dataset: The dataset from which to compute the probabilites (Numpy Array).
        - class_value : Compute the relevant parameters only for instances from the given class.
        """
        self.dataset = dataset
        self.class_value = class_value
    def get_prior(self):
        """
        Returns the prior porbability of the class according to the dataset distribution.
        """
        classInstances = self.dataset[self.dataset[:, -1] == self.class_value]
        return (len(classInstances) / len(self.dataset))
    
    def get_instance_likelihood(self, x):
        """
        Returns the likelihhod porbability of the instance under the class according to the dataset distribution.
        """
        numberOfFeatures = np.size(self.dataset, 1) - 1
        likelihood = 1
        subData = self.dataset[self.dataset[:, -1] == self.class_value]
        for i in range(numberOfFeatures):
            likelihood *= calcLikelihoodForDiscrete(subData, x, i)
        return likelihood

    
    def get_instance_posterior(self, x):
        """
        Returns the posterior porbability of the instance under the class according to the dataset distribution.
        * Ignoring p(x)
        """
        return (self.get_instance_likelihood(x) * self.get_prior())


def calcLikelihoodForDiscrete(subDataset, instance, i):
    possibleValues, numberOfEachValue = np.unique(subDataset[:, i], return_counts=True)
    if instance[i] in possibleValues:
        valueIndex = np.where(possibleValues == instance[i])
        Nij = numberOfEachValue[valueIndex]
    else:
        Nij = 0
    prob = (Nij + 1) / (len(subDataset) + len(possibleValues))

    return prob


####################################################################################################
#                                            General
####################################################################################################            
class MAPClassifier():
    def __init__(self, ccd0 , ccd1):
        """
        A Maximum a postreiori classifier. 
        This class will hold 2 class distribution, one for class 0 and one for class 1, and will predicit and instance
        by the class that outputs the highest posterior probability for the given instance.
    
        Input
            - ccd0 : An object contating the    relevant parameters and methods for the distribution of class 0.
            - ccd1 : An object contating the relevant parameters and methods for the distribution of class 1.
        """
        self.class_0 = ccd0
        self.class_1  = ccd1

    
    def predict(self, x):
        """
        Predicts the instance class using the 2 distribution objects given in the object constructor.
        
        Input
            - An instance to predict.
            
        Output
            - 0 if the posterior probability of class 0 is higher 1 otherwise.
        """
        class_0_post = self.class_0.get_instance_posterior(x)
        class_1_post = self.class_1.get_instance_posterior(x)
        if class_0_post >= class_1_post:
            return 0
        else:
            return 1

    
def compute_accuracy(testset, map_classifier):
    """
    Compute the accuracy of a given a testset and using a map classifier object.
    
    Input
        - testset: The test for which to compute the accuracy (Numpy array).
        - map_classifier : A MAPClassifier object capable of prediciting the class for each instance in the testset.
        
    Ouput
        - Accuracy = #Correctly Classified / #testset size
    """
    numberOfGoodClassification = 0
    for i in range(len(testset)):
        instance = testset[i]
        classificationByMAP = map_classifier.predict(instance)
        if classificationByMAP == instance[-1]:
            numberOfGoodClassification +=1


    return numberOfGoodClassification / len(testset) * 100
    
            
            
            
            
            
            
            
            
            
    