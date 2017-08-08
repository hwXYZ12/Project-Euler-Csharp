// Euler059.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
namespace Euler059
{
    class Euler059
    {
        static void Main()
        {

            // we're going to loop through a few possible keys for the
            // file and use each to produce a decryption and check if it
            // contains some common English words
            string filePath = @"C:\Users\William\Desktop\Exercise Solutions\exerciseProjectCSharp\exerciseProjectCSharp\p059_cipher.txt";
            string encryptedMessage = System.IO.File.ReadAllText(filePath);
            
            // now that we have the message, parse it into its ASCII components
            List<int> asciiCodes = new List<int>();
            while(!encryptedMessage.Equals(""))
            {
                int indexOf = encryptedMessage.IndexOf(',');
                int next = 0;
                if(indexOf == -1)
                {
                    // getting the last integer
                    next = Int32.Parse(encryptedMessage);
                    encryptedMessage = "";
                }
                else
                {
                    next = Int32.Parse(encryptedMessage.Substring(0, indexOf));
                    encryptedMessage = encryptedMessage.Substring(indexOf + 1);
                }
                asciiCodes.Add(next);
            }

            // now we can test each 3 letter password on the encrypted file
            string decrypted;
            StringBuilder sb;
            for (int i = 97; i <= 122; ++i)
            {
                for (int j = 97; j <= 122; ++j)
                {
                    for (int k = 97; k <= 122; ++k )
                    {
                        // using a specific i, j, k combination
                        // we now decrypt the message and check if it
                        // contains one of a few common English phrases
                        sb = new StringBuilder();
                        int[] temp = new int[3];
                        temp[0] = i;
                        temp[1] = j;
                        temp[2] = k;
                        for(int x = 0; x < asciiCodes.Count; ++x)
                        {
                            sb.Append((char)(asciiCodes[x] ^ temp[x % 3]));
                        }
                        decrypted = sb.ToString();
                        if(isEnglish(decrypted))
                        {                         
                            Console.WriteLine(decrypted);
                            int sum = 0;
                            char[] chars = decrypted.ToCharArray();
                            for(int z = 0; z < chars.Length; ++z)
                            {
                                sum += (int) chars[z];
                            }

                            Console.WriteLine("Code: " + (char)i + (char)j + (char)k);
                            Console.WriteLine("Answer: "+sum);

                            // Keep the console window open in debug mode.
                            Console.WriteLine("Press any key to exit.");
                            Console.ReadKey();
                            return;
                        }
                    }
                }                    
            }
                
        }

        static bool isEnglish(string text)
        {
            return text.Contains(" the ");
        }
    }
}