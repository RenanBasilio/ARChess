using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using UnityEngine;

public static class PocketSphinxPINVOKE {

	[DllImport("libpocketsphinxwrap", CallingConvention = CallingConvention.Cdecl)]
	public static extern IntPtr ps_args();

	[DllImport("libpocketsphinxwrap", CallingConvention = CallingConvention.Cdecl)]
	public static extern IntPtr ps_init(IntPtr args_l);
}
