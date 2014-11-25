En postcondition skal skrives så omdannet til kode at den ville kunne fungere.
Det er derfor lettere at lave postcondition i kode først og derefter lave dokumentationen.
I kommentarer kan man ikke skrive < og > derfor brug:
 &lt; lesser than
 &gt; greater than

        /// <summary>
        /// Method documentation
        /// <para> @post match(username, password)</para>
        /// <para> @post exists(username)</para>
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        void LoginAuthentication(User user)
        {
                // Code here
        	#region postconditions
                if(postconditions fails)
                {
                        throw new MeaningfulException("Postcondition fail: With a meaningful message");
                }
        	#endregion postconditions
        }