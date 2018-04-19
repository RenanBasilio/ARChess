using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

public class arg_t : PtrType { public arg_t (IntPtr p) : base(p) { } }
public class cmd_ln_t : PtrType { public cmd_ln_t (IntPtr p) : base(p) { } }

public class PocketSphinxPINVOKE {

	//////////////////////////////// Imports from SphinxBase //////////////////////////////////

	[DllImport("sphinxbase", CallingConvention = CallingConvention.Cdecl)]
	public static extern IntPtr cmd_ln_parse_r (
		IntPtr cmd_ln_t,
		IntPtr arg_t,
		int argc,
		[MarshalAs(UnmanagedType.LPArray)] String[] argv,
		int strict
	);

	/////////////////////////////// Imports from PocketSphinx //////////////////////////////////

	#if UNITY_32 || UNITY_EDITOR_32
		[DllImport("pocketsphinx", CallingConvention = CallingConvention.Cdecl)]
	#elif UNITY_64 || UNITY_EDITOR_64
		[DllImport("libpocketsphinxwrap", CallingConvention = CallingConvention.Cdecl)]
	#endif
	public static extern IntPtr ps_args ();

	#if UNITY_32 || UNITY_EDITOR_32
		[DllImport("pocketsphinx", CallingConvention = CallingConvention.Cdecl)]
	#elif UNITY_64 || UNITY_EDITOR_64
		[DllImport("libpocketsphinxwrap", CallingConvention = CallingConvention.Cdecl)]
	#endif
	public static extern IntPtr ps_init ( IntPtr cmd_ln_t  );
}
