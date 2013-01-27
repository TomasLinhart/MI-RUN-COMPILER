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
    end
  end

end
