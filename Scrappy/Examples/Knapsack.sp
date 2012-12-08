module Scrappy

  class Array
    @Length : Integer
    @ArrayPointer : Integer

    def New(size : Integer) : Array
      emit "iload 1"
      emit "dup"
      emit "iload 0"
      emit "setfield 0"
      emit "newarray"
      emit "iload 0"
      emit "setfield 1"
      emit "return"
    end

    def SetAt(index : Integer, item : Any) : Unit
      if (index <= @Length)
        emit "iload 2" -- item
        emit "iload 1" -- index
        emit "iload 0" -- self
        emit "getfield 1" -- array pointer
        emit "setfield"
        emit "return"
      end
      emit "return"
    end

    def At(index : Integer) : Any
      if (index <= @Length)
        emit "iload 1" -- index
        emit "iload 0" -- self
        emit "getfield 1" -- array pointer
        emit "getfield"
        emit "preturn"
      end
      emit "ppush 0"
      emit "preturn"
    end

    def Copy() : Unit
     -- make copy
	   let bool : Bool = YES
	   let float : Float = 3.14
	   let integer : Integer = 5
	   let string : String = "hello world"
    end

    def ToString() : String
      return "hello, i'm array"
    end

  end

end

module Knapsack

  class EntryPoint

    def Entry(arguments : Array) : Unit -- this method is launched by interpreter
      let item1 : Item = Item#New(1, 2)
      let item2 : Item = Item#New(2, 4)
      let items : Array = Array#New(2)
      items#SetAt(0, item1)
      items#SetAt(1, item2)
      let instance : Instance = Instance#New(2, 5, items)
      -- Console#WriteLine(instance#Solve()#ToString())
	  emit "return"
    end

  end

  class Item
    @Weight : Integer
    @Price : Integer

    def New(weight : Integer, price : Integer) : Item
      @Weight = weight
      @Price = price
    end
  end

  class Instance
    @Quantity : Integer
    @Capacity : Integer
    @Items : Array

    def New(quantity : Integer, capacity : Integer, items : Array) : Instance
      @Quantity = quantity
      @Capacity = capacity
      @Items = items
    end
  end

  class Solver
    @Instance : Instance
    @Items : Array
    @BestPrice : Integer
    @BestSolution : Array -- Array is bult-in type

    def New(instance : Instance) : Solver
      @Instance = instance
      @Items = instance@Items
    end

    def Solve() : Unit
      let array : Array = Array#New(10)

      SolveRecursive(array, 0)

      let i : Integer = 0
      --while (i <) -- TODO: finish it
    end

    def SolveRecursive(array : Array, position : Integer) : Unit
      if (array@Length == position)
        let totalPrice : Integer = 0
        let totalWeight : Integer = 0

        let i : Integer = 0
        while (i < array@Length)
          if (totalWeight > @Instance)
            return
          end

          if ((Integer) array#At(i) == YES)
			let item : Item = (Item) @Items#At(i)
            totalPrice = totalPrice + item@Price
            totalWeight = totalWeight + item@Weight
          end

          i = i + 1
        end

        if (totalWeight <= @Instance@Capacity && totalPrice > @BestPrice)
          @BestSolution = Array#Copy()
          @BestPrice = totalPrice
        end

        array#SetAt(position, NO)
        SolveRecursive(array, position + 1)
        array#SetAt(position, YES)
        SolveRecursive(array, position + 1)
      end
    end

    def ToString() : String
      return @BestSolution#ToString()
    end

  end

end
