﻿/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2015 JWC s.r.o. <http://www.jwc.sk>
 *  Author: Jaroslav Imrich <jimrich@jimrich.sk>
 *
 *  Licensing for open source projects:
 *  Pkcs11Interop is available under the terms of the GNU Affero General 
 *  Public License version 3 as published by the Free Software Foundation.
 *  Please see <http://www.gnu.org/licenses/agpl-3.0.html> for more details.
 *
 *  Licensing for other types of projects:
 *  Pkcs11Interop is available under the terms of flexible commercial license.
 *  Please contact JWC s.r.o. at <info@pkcs11interop.net> for more details.
 */

using System;
using System.Runtime.InteropServices;

namespace Net.Pkcs11Interop.LowLevelAPI41.MechanismParams
{
    /// <summary>
    /// Structure that provides the parameters for the CKM_ECDH1_DERIVE and CKM_ECDH1_COFACTOR_DERIVE key derivation mechanisms
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_ECDH1_DERIVE_PARAMS
    {
        /// <summary>
        /// Key derivation function used on the shared secret value (CKD)
        /// </summary>
        public uint Kdf;

        /// <summary>
        /// The length in bytes of the shared info
        /// </summary>
        public uint SharedDataLen;

        /// <summary>
        /// Some data shared between the two parties
        /// </summary>
        public IntPtr SharedData;

        /// <summary>
        /// The length in bytes of the other party's EC public key
        /// </summary>
        public uint PublicDataLen;

        /// <summary>
        /// Pointer to other party's EC public key value
        /// </summary>
        public IntPtr PublicData;
    }
}
