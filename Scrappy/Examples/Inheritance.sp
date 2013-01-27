module Inheritance

	class A
		@A : Integer

		def Test() : String
			return "A"
		end
		
		def Test(a: Integer, b: A) : String
			return "with A"
		end
	end

	class B : A
		@B : Integer

		def Test() : String
			return "B"
		end

		def MyTest() : String
			return parent::Test()
		end

		def SecondTest() : String
			return A::Test()
		end
		
		def Test(a: Integer, b: B) : String
			return "with B"
		end
	end
	
	class C : B
		@C : Integer
	
	end
	
	class EntryPoint

    def Entry(args : Array) : Unit -- this method is launched by interpreter
		let b : B = B#New()
		b#Test()
		emit "syscall 1" -- print  string
		b#MyTest()
		emit "syscall 1" -- print  string
		b#SecondTest()
		emit "syscall 1" -- print  string
		let a : A = A#New()
		a = b
		a#Test()
		emit "syscall 1" -- print  string
		a = A#New()
		let c : C = C#New()
		c#Test(1, a)
		emit "syscall 1" -- print  string
		c#Test(1, b)
		emit "syscall 1" -- print  string
    end
  end

end
