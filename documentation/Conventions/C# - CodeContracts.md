#Code Contracts conventions
Install this package into Visual Studio:
	`https://visualstudiogallery.msdn.microsoft.com/1ec7db13-3363-46c9-851f-1ce455f66970`

Make ReSharper understand contracts:

* Right-click project which uses contracts.
* Choose `Properties`
* Choose `Build` in the sidebar
* Write `CONTRACTS_FULL` (or `CONTRACTS FULL`, not sure which one works) in `Conditional Compilation Symbols`

##Precondition

	public string Value()
	{
		Contract.Requires<InvalidOperationException>(
			_value != null,
			"_value is Null");
		return _value;
	}

###Legacy Preconditions
If legacy preconditions (`if-then-throw`) is used the contracts block must be terminated by `EndContractBlock()`:

	public string Value()
	{
		if (value == null) throw new InvalidOperationException("_value");
		Contract.EndContractBlock();
		return _value;
	}

##Postcondition
	public string Value()
	{
		Contract.Ensures(Contract.Result<string>() != null); // I think it works like this.
		return _value;
	}

###Postcondition when exception is thrown
	public string Value()
	{
		Contract.EnsuresOnThrow<InvalidOperationException>(_value != null);
		CallToMethodWhichMightThrowInvalidOperationException();
		return _value;
	}

##Invariant
	[ContractInvariantMethod]
	protected void ObjectInvariant () 
	{
		Contract.Invariant ( this.y >= 0 );
		Contract.Invariant ( this.x > this.y );
		...
	}

If `CONTRACTS_FULL`/`CONTRACTS FULL` is specified and runtime-contract-checking is enabled, this method will be called everytime a public method returns.
If a public method is used in the `ObjectInvariant`-method it won't loop forever if I understand it right.

#Sources
	http://msdn.microsoft.com/en-us/library/dd264808.aspx