/*
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
    /// Structure that provides the parameters to the CKM_X9_42_MQV_DERIVE key derivation mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_X9_42_MQV_DERIVE_PARAMS
    {
        /// <summary>
        /// Key derivation function used on the shared secret value (CKD)
        /// </summary>
        public uint Kdf;

        /// <summary>
        /// The length in bytes of the other info
        /// </summary>
        public uint OtherInfoLen;

        /// <summary>
        /// Some data shared between the two parties
        /// </summary>
        public IntPtr OtherInfo;

        /// <summary>
        /// The length in bytes of the other party's first X9.42 Diffie-Hellman public key
        /// </summary>
        public uint PublicDataLen;

        /// <summary>
        /// Pointer to other party's first X9.42 Diffie-Hellman public key value
        /// </summary>
        public IntPtr PublicData;

        /// <summary>
        /// The length in bytes of the second X9.42 Diffie-Hellman private key
        /// </summary>
        public uint PrivateDataLen;

        /// <summary>
        /// Key handle for second X9.42 Diffie-Hellman private key value
        /// </summary>
        public uint PrivateData;

        /// <summary>
        /// The length in bytes of the other party's second X9.42 Diffie-Hellman public key
        /// </summary>
        public uint PublicDataLen2;

        /// <summary>
        /// Pointer to other party's second X9.42 Diffie-Hellman public key value
        /// </summary>
        public IntPtr PublicData2;

        /// <summary>
        /// Handle to the first party's ephemeral public key
        /// </summary>
        public uint PublicKey;
    }
}
