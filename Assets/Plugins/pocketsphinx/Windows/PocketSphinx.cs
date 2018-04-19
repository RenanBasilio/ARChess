using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketSphinx {

	public static arg_t getArgumentDefinitions() { return new arg_t(PocketSphinxPINVOKE.ps_args()); }

	public static cmd_ln_t buildDecoderArgs(cmd_ln_t decoderArgs, arg_t argDefinitions, String[] args) { 
		IntPtr r_cmd_ln_t = PocketSphinxPINVOKE.cmd_ln_parse_r(decoderArgs, argDefinitions, args.Length, args, 1);
		return new cmd_ln_t(r_cmd_ln_t);
	}

}


public class Decoder {

	private IntPtr decoder;

	public Decoder (cmd_ln_t decoderArgs) {
		try {
			decoder = PocketSphinxPINVOKE.ps_init(decoderArgs);
		} catch (Exception e) {
			Debug.Log(e.Message);
		}

	}
}
