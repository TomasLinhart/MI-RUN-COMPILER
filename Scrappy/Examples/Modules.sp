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
