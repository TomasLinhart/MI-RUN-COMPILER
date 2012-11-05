module A
	
	class Test
	
		@hello : Integer
		@world : String
		@generic : Array[Item] -- only one generic parameter is supported

		def method() : Unit
			ahoj = @generic@world@hello
		end
	
	end

end

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
end
