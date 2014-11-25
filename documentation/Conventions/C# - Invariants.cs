En invariant skal skrives så omdannet til kode at den ville kunne fungere.
Det er derfor lettere at lave postcondition i kode først og derefter lave dokumentationen.
I kommentarer kan man ikke skrive < og > derfor brug:
 &lt; lesser than
 &gt; greater than

    /// <summary>
    /// Class documentation
    /// <para> @inv (1990,1,1) &lt;= DateTime &lt;= (2100,1,1)  </para>
    /// <para> @inv this.ID &lt; storage.MaxValue(ID) </para>
    /// <para> @inv this.ID &gt; -1 </para>
    /// </summary>
    public class Event : IEvent
    {
        private void CheckInvariants()
        {
                // Do invariants check
                if(invariant fails)
                {
                        throw new MeaningfulException("Ivariant fail: With a meaningful message");
                }
        }
    }