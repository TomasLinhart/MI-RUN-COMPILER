module Scrappy

  class List
    @Length : Integer
    @ArrayPointer : Array

    def New() : List
      emit "getfield 1"
      emit "invokevirtual ::size:"
      emit "vload 0"
      emit "setfield 0"
      emit "return"
    end

    def New(size : Integer) : List
      emit "vload 1" -- load size
      emit "dup" -- duplicate it
      emit "vload 0" -- load self
      emit "setfield 0" -- set first field
      emit "newarray" -- alloc new array, size is from iload 1
      emit "vload 0" -- load self
      emit "setfield 1" -- set it on self
      emit "return"
    end

    def SetAt(index : Integer, item : Any) : Unit
      if (index <= @Length)
        emit "vload 1" -- index
        emit "vload 2" -- item
        emit "vload 0" -- self
        emit "getfield 1" -- array pointer
        emit "setfield"
      end
    end

    def At(index : Integer) : Any
        emit "vload 1" -- index
        emit "vload 0" -- self
        emit "getfield 1" -- array pointer
        emit "getfield"
        emit "vreturn"
    end

    def Copy() : Unit
    	let newList : List = List#New(@Length)
    	
    	let i : Integer = 0
    	while (i < @Length)
    	   newList#SetAt(i, At(i))
    	   i = i + 1
    	end
    	
    	return newList
    end

  end

end

module Knapsack

  class EntryPoint

    def Entry(args : Array) : Unit -- this method is launched by interpreter
      let reader : FileReader = FileReader#New()
      reader#open("data.txt")
      let line : String = reader#readLine()
      let parts : Array = line#split()
      let listParts : List = List#New(parts#size())
      listParts@ArrayPointer = parts

	  let itemsCountString : String = (String) listParts#At(0)
	  let capacityString : String = (String) listParts#At(1)
	  let itemsCount : Integer = itemsCountString#toInteger()
	  let capacity : Integer = capacityString#toInteger()
	  let items : List = List#New(itemsCount)
	  let instance : Instance = Instance#New(itemsCount, capacity, items)
	  let i : Integer = 2
	  let j : Integer = 0
	  
	  while (i < parts#size())
	  	let weightString : String = (String) listParts#At(i)
	  	let priceString  : String = (String) listParts#At(i + 1)
	  	let weight : Integer = weightString#toInteger()
	  	let price : Integer = priceString#toInteger()
	  	let item : Item = Item#New(weight, price)
	  	items#SetAt(j, item)
	  	i = i + 2
	  	j = j + 1
	  end
	  
	  let solver : Solver = Solver#New(instance)
      let bestSolution : List = solver#Solve()
            
      i = 0
      let result : String = " "
      result = result#append(solver@BestPrice)
      while (i < bestSolution@Length)
        result = result#append(" ")
        result = result#append((Integer) bestSolution#At(i))
      	i = i + 1
      end
                  
      let writer : FileWriter = FileWriter#New()
      writer#open("result.txt")
      writer#writeLine(result)
      
      reader#close()
      writer#close()
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
    @Items : List

    def New(quantity : Integer, capacity : Integer, items : List) : Instance
      @Quantity = quantity
      @Capacity = capacity
      @Items = items
    end
  end

  class Solver
    @Instance : Instance
    @Items : List
    @BestPrice : Integer
    @BestSolution : List

    def New(instance : Instance) : Solver
      @Instance = instance
      @Items = instance@Items
    end

    def Solve() : Unit
      let array : List = List#New(@Instance@Quantity)
	  @BestSolution = array
	  @BestPrice = 0
	  
      SolveRecursive(array, 0)
            
      return @BestSolution
    end

    def SolveRecursive(array : List, position : Integer) : Unit
      if (array@Length == position)
        let totalPrice : Integer = 0
        let totalWeight : Integer = 0
        
        let i : Integer = 0
        while (i < array@Length)
          if (totalWeight > @Instance@Capacity)
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
          @BestSolution = array#Copy()
          @BestPrice = totalPrice
        end
        
        return
      end

      array#SetAt(position, NO)
      SolveRecursive(array, position + 1)
      array#SetAt(position, YES)
      SolveRecursive(array, position + 1)
    end

  end

end
