using System;

/**
 * This class implements an implicitly convertible interface to avoid passing incorrect
 * to avoid passing incorrect arguments to methods through PINVOKE interfaces.
 */
public class PtrType {
	IntPtr pointer;

	public static implicit operator System.IntPtr ( PtrType p ) {
		return p.pointer;
	}

	public static implicit operator PtrType ( System.IntPtr p) {
		return new PtrType(p);
	}

	protected PtrType( IntPtr p ) {
		pointer = p;
	}
}
