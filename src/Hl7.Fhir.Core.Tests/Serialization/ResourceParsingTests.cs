﻿/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Serialization;
using System.IO;

namespace Hl7.Fhir.Tests.Serialization
{
    [TestClass]
#if PORTABLE45
	public class PortableResourceParsingTests
#else
	public class ResourceParsingTests
#endif
    {
        [TestMethod]
        //public void AcceptXsiStuffOnRoot()
        public void ConfigureFailOnUnknownMember()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir'><daytona></daytona></Patient>";

            try
            {
                (new FhirParser()).ParseResourceFromXml(xml);
                Assert.Fail("Should have failed on unknown member");
            }
            catch (FormatException)
            {
            }

            SerializationConfig.AcceptUnknownMembers = true;
            (new FhirParser()).ParseResourceFromXml(xml);
        }


        [TestMethod]
        public void AcceptXsiStuffOnRoot()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' " +
                            "xsi:schemaLocation='http://hl7.org/fhir ../../schema/fhir-all.xsd'></Patient>";

            (new FhirParser()).ParseResourceFromXml(xml);

            SerializationConfig.EnforceNoXsiAttributesOnRoot = true;

            try
            {
                (new FhirParser()).ParseResourceFromXml(xml);
                Assert.Fail("Should have failed on xsi: elements in root");
            }
            catch (FormatException)
            {
            }
        }


        [TestMethod]
        public void EdgecaseRoundtrip()
        {
            string json = File.ReadAllText(@"TestData\edgecases.json");

            var poco = (new FhirParser()).ParseResourceFromJson(json);
            Assert.IsNotNull(poco);
            var xml = (new FhirSerializer()).SerializeResourceToXml(poco);
            Assert.IsNotNull(xml);
            File.WriteAllText(@"c:\temp\edgecase.xml", xml);

            poco = (new FhirParser()).ParseResourceFromXml(xml);
            Assert.IsNotNull(poco);
            var json2 = (new FhirSerializer()).SerializeResourceToJson(poco);
            Assert.IsNotNull(json2);
            File.WriteAllText(@"c:\temp\edgecase.json", json2);
           
            JsonAssert.AreSame(json, json2);
        }


        // TODO: Unfortunately, this is currently too much work to validate. See comments on the bottom of
        // https://github.com/ewoutkramer/fhir-net-api/issues/20
        [TestMethod, Ignore]
        public void CatchArrayWithNull()
        {
            var json = @"{
                'resourceType': 'Profile',
                'identifier': 'oh1394156991825',
                'name': 'a new profile',
                'publisher': 'Orion Health',
                'description': 'xcv',
                'status': 'draft',
                'experimental': true,
                'date': '2014-03-07T14:49:51+13:00',
                'requirements': 'cxv',
                'extensionDefn': [{
                    'code': 'test1',
                    'contextType': 'resource',
                    'context': [null],
                    'definition': {
                        'short': 'should change - and again',
                        'formal': '',
                        'min': '0',
                        'max': '1',
                        'type': [{'code': 'date'}],
                        'isModifier': false,
                        'binding': {
                            'name': 'ValueSet1',
                            'referenceResource': {'reference': 'http://spark.furore.com/fhir/ValueSet/3216371'}
                        }
                    },
                    }]}";

            try
            {
                var prof = (new FhirParser()).ParseResourceFromJson(json);
                Assert.Fail("Should have failed parsing");
            }
            catch (FormatException)
            {

            }
        }
    }
}
