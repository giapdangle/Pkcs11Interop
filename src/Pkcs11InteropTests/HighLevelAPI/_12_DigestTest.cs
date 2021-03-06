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
using System.IO;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// Digesting tests.
    /// </summary>
    [TestFixture()]
    public class _12_DigestTest
    {
        /// <summary>
        /// Single-part digesting test.
        /// </summary>
        [Test()]
        public void _01_DigestSinglePartTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO session
                using (Session session = slot.OpenSession(true))
                {
                    // Specify digesting mechanism
                    Mechanism mechanism = new Mechanism(CKM.CKM_SHA_1);
                    
                    byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");
                    
                    // Digest data
                    byte[] digest = session.Digest(mechanism, sourceData);
                    
                    // Do something interesting with digest value
                    Assert.IsTrue(Convert.ToBase64String(digest) == "e1AsOh9IyGCa4hLN+2Od7jlnP14=");
                }
            }
        }

        /// <summary>
        /// Multi-part digesting test.
        /// </summary>
        [Test()]
        public void _02_DigestMultiPartTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO session
                using (Session session = slot.OpenSession(true))
                {
                    // Specify digesting mechanism
                    Mechanism mechanism = new Mechanism(CKM.CKM_SHA_1);
                    
                    byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");
                    byte[] digest = null;
                    
                    // Multipart digesting can be used i.e. for digesting of streamed data
                    using (MemoryStream inputStream = new MemoryStream(sourceData))
                    {
                        // Digest data
                        digest = session.Digest(mechanism, inputStream);
                    }

                    // Do something interesting with digest value
                    Assert.IsTrue(Convert.ToBase64String(digest) == "e1AsOh9IyGCa4hLN+2Od7jlnP14=");
                }
            }
        }
        
        /// <summary>
        /// DigestKey test.
        /// </summary>
        [Test()]
        public void _03_DigestKeyTest()
        {
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
                    ObjectHandle generatedKey = Helpers.GenerateKey(session);

                    // Specify digesting mechanism
                    Mechanism mechanism = new Mechanism(CKM.CKM_SHA_1);
                    
                    // Digest key
                    byte[] digest = session.DigestKey(mechanism, generatedKey);
                    
                    // Do something interesting with digest value
                    Assert.IsNotNull(digest);

                    session.DestroyObject(generatedKey);
                    session.Logout();
                }
            }
        }
    }
}

