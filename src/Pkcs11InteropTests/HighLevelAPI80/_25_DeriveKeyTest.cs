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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI80;
using Net.Pkcs11Interop.HighLevelAPI80.MechanismParams;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI80
{
    /// <summary>
    /// DeriveKey tests.
    /// </summary>
    [TestFixture()]
    public class _25_DeriveKeyTest
    {
        /// <summary>
        /// DeriveKey test.
        /// </summary>
        [Test()]
        public void _01_BasicDeriveKeyTest()
        {
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 0)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW session
                using (Session session = slot.OpenSession(false))
                {
                    // Login as normal user
                    session.Login(CKU.CKU_USER, Settings.NormalUserPin);
                    
                    // Generate symetric key
                    ObjectHandle baseKey = Helpers.GenerateKey(session);

                    // Generate random data needed for key derivation
                    byte[] data = session.GenerateRandom(24);

                    // Specify mechanism parameters
                    CkKeyDerivationStringData mechanismParams = new CkKeyDerivationStringData(data);

                    // Specify derivation mechanism with parameters
                    Mechanism mechanism = new Mechanism(CKM.CKM_XOR_BASE_AND_DATA, mechanismParams);
                    
                    // Derive key
                    ObjectHandle derivedKey = session.DeriveKey(mechanism, baseKey, null);

                    // Do something interesting with derived key
                    Assert.IsTrue(derivedKey.ObjectId != CK.CK_INVALID_HANDLE);

                    session.DestroyObject(baseKey);
                    session.DestroyObject(derivedKey);
                    session.Logout();
                }
            }
        }
    }
}

