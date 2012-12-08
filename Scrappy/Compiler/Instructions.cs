using System;

namespace Scrappy.Compiler
{
	public static class Instructions
	{
	    public static readonly string PushIntInstruction = "ipush";
        public static readonly string PushPointerInstruction = "ppush";
		public static readonly string SyscallInstruction = "syscall";
		public static readonly string NewInstruction = "new";
		public static readonly string CallInstruction = "invokevirtual";
		public static readonly string GetFieldInstruction = "getfield";
		public static readonly string SetFieldInstruction = "setfield";
		public static readonly string ReturnIntInstruction = "ireturn";
		public static readonly string ReturnPointerInstruction = "preturn";
		public static readonly string ReturnInstruction = "return";
		public static readonly string AddIntInstruction = "iadd";
		public static readonly string SubIntInstruction = "isub";
		public static readonly string MulIntInstruction = "imul";
		public static readonly string DivIntInstruction = "idiv";
		public static readonly string ModIntInstruction = "imod";
		public static readonly string IfIntEqInstruction = "ifeq";
		public static readonly string IfIntGeInstruction = "ifge";
		public static readonly string IfIntGtInstruction = "ifgt";
		public static readonly string IfIntLeInstruction = "ifle";
		public static readonly string IfIntLtInstruction = "iflt";
		public static readonly string IfIntNeqInstruction = "ifneq";
		public static readonly string IfPointerNotNullInstruction = "ifnotnull";
		public static readonly string IfPointerNullInstruction = "ifnull";
		public static readonly string NewArrayInstruction = "newarray";
		public static readonly string LoadIntInstruction = "iload";
		public static readonly string LoadPointerInstruction = "pload";
		public static readonly string StoreIntInstruction = "istore";
		public static readonly string StorePointerInstruction = "pstore";
		public static readonly string JumpInstruction = "jump";
		public static readonly string ArrayLengthInstruction = "arraylength";
		public static readonly string NewStringInstruction = "newstring";
	    public static readonly string DupInstruction = "dup";
        public static readonly string LogicalAndIntInstruction = "iand";
        public static readonly string LogicalOrIntInstruction = "ior";
        public static readonly string LogicalNegIntInstruction = "ineg";
	}
}
