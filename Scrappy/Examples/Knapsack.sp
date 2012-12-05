module Knapsack

  class EntryPoint

    def Entry(arguments : Array[String]) : Unit -- this method is launched by interpreter
      let item1 : Item = Item#New(1, 2)
      let item2 : Item = Item#New(2, 4)
      let items : Array[Item] = Array#New()
      items#SetAt(0, item1)
      items#SetAt(1, item2)
      let instance : Instance = Instance#New(2, 5, items)
      Console#WriteLine(instance#Solve()#ToString())
    end

  end

  class Item
    @Weight : Integer
    @Price : Integer

    def New(weight : Integer, price : Integer) : Item
      @Weight = weight
      @Price = price
      return @Self
    end
  end

  class Instance
    @Quantity : Integer
    @Capacity : Integer
    @Items : Array[Item]

    def New(quantity : Integer, capacity : Integer, items : Array[Item]) : Instance
      @Quantity = quantity
      @Capacity = capacity
      @Items = items
      return @Self
    end
  end

  class Solver
    @Instance : Instance
    @Items : Array[Item]
    @BestPrice : Integer
    @BestSolutin : Array[Bool] -- Array is bult-in type

    def New(instance : Instance) : Solver
      @Instance = instance
      @Items = instance@Items
    end

    def Solve() : Unit
      let array : Array[Item] = Array[Item]#New(10)

      @Self#SolveRecursive(array, 0)

      let i : Integer = 0
      --while (i <)
    end

    def SolveRecursive(array : Array[Bool], position : Integer) : Unit
      if (array@Length == position)
        let totalPrice : Integer = 0
        let totalWeight : Integer = 0

        let i : Integer = 0
        while (i < array@Length)
          if (totalWeight > @Instance)
            return Unit
          end

          if (array#At(i) == YES)
            totalPrice = Integer#Plus(totalPrice, items#At(i)@Price)
            totalWeight = Integer#Plus(totalWeight, items#At(i)@Weight)
          end

          i = Integer#plus(i, 1)
        end

        if (totalWeight <= @Instance@Capacity && totalPrice > @BestPrice)
          Array#Copy(array, @BestSolution, array@Length)
          @BestPrice = totalPrice
        end

        array#SetAt(position, NO)
        @Self#SolveRecursive(array, Integer#Add(position, 1))
        array#SetAt(position, YES)
        @Self#SolveRecursive(array, Integer#Add(position, 1))
      end
    end

    def ToString() : String
      return @BestSolution#ToString()
    end

  end

end
