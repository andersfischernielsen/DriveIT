En precondition skal skrives så omdannet til kode at den ville kunne fungere.
Det er derfor lettere at lave precondition i kode først og derefter lave dokumentationen.
I kommentarer kan man ikke skrive < og > derfor brug:
 &lt; lesser than
 &gt; greater than

        /// <summary>
        /// Method documentation
        /// <para> @pre match(username, password)</para>
        /// <para> @pre exists(username)</para>
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        void LoginAuthentication(User user)
        {
        	#region preconditions
        	if(postconditions fails)
            {
                throw new MeaningfulException("Precondition fail: With a meaningful message");
            }
        	#endregion preconditions
        	// Code here
        }