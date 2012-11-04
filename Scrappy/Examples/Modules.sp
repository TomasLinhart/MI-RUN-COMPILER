module A
	
	class Test
	
		@hello : Integer
		@world : String
		@generic : Array[Item] -- only one generic parameter is supported

		def method() : Unit

		end
	
	end

end

module Knapsack
	class Item
		@Weight : Integer
		@Price : Integer

		def New(weight : Integer, price : Integer) : Item
		--	@Weight = weight
		--	@Price = price
		--	return @Self
		end
	end
end
