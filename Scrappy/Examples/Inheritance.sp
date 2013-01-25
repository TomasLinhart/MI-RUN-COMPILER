module Inheritance

	class A
		def Test() : String
			return "A"
		end
	end

	class B : A
		@Super : A

		def Test() : String
			return "B"
		end

		def MyTest() : String
			return @Super#Test()
		end
	end

end
