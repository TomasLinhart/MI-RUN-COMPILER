module Inheritance

	class A
		def Test() : String
			return "A"
		end
	end

	class B : A
		def Test() : String
			return "B"
		end

		def MyTest() : String
			return parent::Test()
		end

		def SecondTest() : String
			return A::Test()
		end
	end

end
