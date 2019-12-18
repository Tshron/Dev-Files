using System;

// $G$ RUL-004 (-20) Wrong zip name format / folder name format.

// $G$ SFN-002 (-3) Selecting an action item should clear the screen.

namespace Ex04.Menus.Test
{
    public class Program
    {
        public static void Main()
        {
            BeginTests();
        }

        // $G$ CSS-010 (-3) Private method should start with a lower case letter.
        private static void BeginTests()
        {
            int MenuDepth = 1;

            TestInterface testInterface = new TestInterface();
            Interfaces.MainMenu interfaceMainMenu = testInterface.BuildMenu();
            testInterface.LoopMenu(interfaceMainMenu, MenuDepth);

            TestDelegates testDelegates = new TestDelegates();
            Delegates.MainMenu delegatesMainMenu = testDelegates.BuildMenu();
            testDelegates.LoopMenu(delegatesMainMenu, MenuDepth);

            Console.WriteLine("Menu Closed");
            Console.Read();
        }
    }
}
