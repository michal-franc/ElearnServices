namespace elearn
{
    public class WebHelper
    {
        private static string loremIpsum =@"Lorem ipsum dolor sit amet, consectetur adipisicing elit, 
                sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
                Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris
                nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in 
                reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla 
                pariatur. Excepteur sint occaecat cupidatat non proident, sunt in 
                culpa qui officia deserunt mollit anim id est laborum.";

        public static string CreateLoremIpsum(int multi)
        {
            string returnString = System.String.Empty;

            for(int i=0;i<multi;i++)
            {
                returnString += loremIpsum;
            }

            return returnString;
        }
    }
}