import { Configuration } from '@config/config.model';

export class MockConfig {
  public static get(): Configuration {
    /* eslint-disable */
    // Export of /lookups response copied from PostMan:
    return {
      'colleges': [
        {
          'code': 1,
          'name': 'College of Physicians and Surgeons of BC (CPSBC)',
          'weight': 10,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 1,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 2,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 3,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 4,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 5,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 6,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 7,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 8,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 9,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 10,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 11,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 12,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 13,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 14,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 15,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 16,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 17,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 18,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 19,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 20,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 21,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 22,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 23,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 24,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 59,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 65,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 66,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 1,
              'licenseCode': 67,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'collegePractices': []
        },
        {
          'code': 2,
          'name': 'College of Pharmacists of BC (CPBC)',
          'weight': 11,
          'collegeLicenses': [
            {
              'collegeCode': 2,
              'licenseCode': 25,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 2,
              'licenseCode': 26,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 2,
              'licenseCode': 27,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 2,
              'licenseCode': 28,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 2,
              'licenseCode': 29,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 2,
              'licenseCode': 30,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 2,
              'licenseCode': 31,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 2,
              'licenseCode': 68,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'collegePractices': []
        },
        {
          'code': 3,
          'name': 'BC College of Nurses and Midwives (BCCNM)',
          'weight': 12,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 32,
              'collegeLicenseGroupingCode': 2,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 33,
              'collegeLicenseGroupingCode': 2,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 34,
              'collegeLicenseGroupingCode': 2,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 35,
              'collegeLicenseGroupingCode': 2,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 36,
              'collegeLicenseGroupingCode': 2,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 37,
              'collegeLicenseGroupingCode': 2,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 39,
              'collegeLicenseGroupingCode': 2,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 40,
              'collegeLicenseGroupingCode': 2,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 41,
              'collegeLicenseGroupingCode': 3,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 42,
              'collegeLicenseGroupingCode': 3,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 43,
              'collegeLicenseGroupingCode': 3,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 45,
              'collegeLicenseGroupingCode': 3,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 46,
              'collegeLicenseGroupingCode': 3,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 47,
              'collegeLicenseGroupingCode': 4,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 48,
              'collegeLicenseGroupingCode': 4,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 49,
              'collegeLicenseGroupingCode': 4,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 51,
              'collegeLicenseGroupingCode': 4,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 52,
              'collegeLicenseGroupingCode': 1,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 53,
              'collegeLicenseGroupingCode': 1,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 54,
              'collegeLicenseGroupingCode': 1,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 55,
              'collegeLicenseGroupingCode': 1,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 60,
              'collegeLicenseGroupingCode': 5,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 61,
              'collegeLicenseGroupingCode': 5,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 62,
              'collegeLicenseGroupingCode': 5,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 63,
              'collegeLicenseGroupingCode': 5,
              'discontinued': false
            },
            {
              'collegeCode': 3,
              'licenseCode': 69,
              'collegeLicenseGroupingCode': 5,
              'discontinued': false
            }
          ],
          'collegePractices': [
            {
              'collegeCode': 3,
              'practiceCode': 1
            },
            {
              'collegeCode': 3,
              'practiceCode': 2
            },
            {
              'collegeCode': 3,
              'practiceCode': 3
            },
            {
              'collegeCode': 3,
              'practiceCode': 4
            }
          ]
        },
        {
          'code': 4,
          'name': 'College of Chiropractors of BC',
          'weight': 14,
          'collegeLicenses': [
            {
              'collegeCode': 4,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'collegePractices': []
        },
        {
          'code': 5,
          'name': 'College of Dental Hygenists of BC',
          'weight': 15,
          'collegeLicenses': [
            {
              'collegeCode': 5,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'collegePractices': []
        },
        {
          'code': 6,
          'name': 'College of Dental Technicians of BC',
          'weight': 16,
          'collegeLicenses': [
            {
              'collegeCode': 6,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'collegePractices': []
        },
        {
          'code': 7,
          'name': 'College of Dental Surgeons of BC',
          'weight': 16,
          'collegeLicenses': [
            {
              'collegeCode': 7,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'collegePractices': []
        },
        {
          'code': 8,
          'name': 'College of Denturists of BC',
          'weight': 17,
          'collegeLicenses': [
            {
              'collegeCode': 8,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'collegePractices': []
        },
        {
          'code': 9,
          'name': 'College of Dietitians of BC',
          'weight': 18,
          'collegeLicenses': [
            {
              'collegeCode': 9,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'collegePractices': []
        },
        {
          'code': 10,
          'name': 'College of Massage Therapists of BC',
          'weight': 19,
          'collegeLicenses': [
            {
              'collegeCode': 10,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'collegePractices': []
        },
        {
          'code': 11,
          'name': 'College of Naturopathic Physicians of BC',
          'weight': 20,
          'collegeLicenses': [
            {
              'collegeCode': 11,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'collegePractices': []
        },
        {
          'code': 12,
          'name': 'College of Occupational Therapists of BC',
          'weight': 21,
          'collegeLicenses': [
            {
              'collegeCode': 12,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'collegePractices': []
        },
        {
          'code': 13,
          'name': 'College of Opticians of BC',
          'weight': 22,
          'collegeLicenses': [
            {
              'collegeCode': 13,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'collegePractices': []
        },
        {
          'code': 14,
          'name': 'College of Optometrists of BC',
          'weight': 23,
          'collegeLicenses': [
            {
              'collegeCode': 14,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'collegePractices': []
        },
        {
          'code': 15,
          'name': 'College of Physical Therapists of BC',
          'weight': 24,
          'collegeLicenses': [
            {
              'collegeCode': 15,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'collegePractices': []
        },
        {
          'code': 16,
          'name': 'College of Psychologists of BC',
          'weight': 25,
          'collegeLicenses': [
            {
              'collegeCode': 16,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'collegePractices': []
        },
        {
          'code': 17,
          'name': 'College of Speech and Hearing Health Professionals of BC',
          'weight': 26,
          'collegeLicenses': [
            {
              'collegeCode': 17,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'collegePractices': []
        },
        {
          'code': 18,
          'name': 'College of Traditional Chinese Medicine Practitioners and Acupuncturists of BC',
          'weight': 27,
          'collegeLicenses': [
            {
              'collegeCode': 18,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'collegePractices': []
        }
      ],
      'jobNames': [
        {
          'code': 4,
          'name': 'Ward clerk'
        },
        {
          'code': 3,
          'name': 'Registration clerk'
        },
        {
          'code': 5,
          'name': 'Nursing unit assistant'
        },
        {
          'code': 1,
          'name': 'Medical office assistant'
        },
        {
          'code': 2,
          'name': 'Pharmacy assistant'
        }
      ],
      'licenses': [
        {
          'code': 1,
          'weight': 1,
          'prefix': '91',
          'manual': false,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Full - Family',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 1,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 2,
          'weight': 2,
          'prefix': '91',
          'manual': false,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Full - Specialty',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 2,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null

        },
        {
          'code': 3,
          'weight': 12,
          'prefix': '91',
          'manual': true,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Special',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 3,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null

        },
        {
          'code': 4,
          'weight': 11,
          'prefix': '91',
          'manual': false,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Osteopathic',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 4,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 5,
          'weight': 3,
          'prefix': '91',
          'manual': false,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Provisional - Family',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 5,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 6,
          'weight': 4,
          'prefix': '91',
          'manual': false,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Provisional - Specialty',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 6,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 7,
          'weight': 10,
          'prefix': '91',
          'manual': true,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Academic',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 7,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 8,
          'weight': 6,
          'prefix': '91',
          'manual': true,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Conditional - Practice Limitations',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 8,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 9,
          'weight': 5,
          'prefix': '91',
          'manual': true,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Conditional - Practice Setting',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 9,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 10,
          'weight': 7,
          'prefix': '91',
          'manual': true,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Conditional - Disciplined',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 10,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 11,
          'weight': 18,
          'prefix': '91',
          'manual': false,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Educational - Medical Student',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 11,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 12,
          'weight': 14,
          'prefix': '91',
          'manual': false,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Educational - Postgraduate Resident',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 12,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 13,
          'weight': 15,
          'prefix': '91',
          'manual': false,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Educational - Postgraduate Resident Elective',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 13,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 14,
          'weight': 16,
          'prefix': '91',
          'manual': false,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Educational - Postgraduate Fellow',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 14,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 15,
          'weight': 17,
          'prefix': '91',
          'manual': false,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Educational - Postgraduate Trainee',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 15,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 16,
          'weight': 9,
          'prefix': '91',
          'manual': false,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Clinical Observership',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 16,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 17,
          'weight': 13,
          'prefix': '91',
          'manual': true,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Visitor',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 17,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 18,
          'weight': 22,
          'prefix': '91',
          'manual': false,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Emergency - Family',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 18,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 19,
          'weight': 23,
          'prefix': '91',
          'manual': false,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Emergency - Specialty',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 19,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 20,
          'weight': 20,
          'prefix': '91',
          'manual': true,
          'validate': true,
          'namedInImReg': false,
          'licensedToProvideCare': false,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Retired - Life ',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 20,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 21,
          'weight': 24,
          'prefix': '91',
          'manual': true,
          'validate': true,
          'namedInImReg': false,
          'licensedToProvideCare': false,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Temporarily Inactive',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 21,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 22,
          'weight': 8,
          'prefix': '91',
          'manual': false,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Surgical Assistant',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 22,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 23,
          'weight': 19,
          'prefix': '91',
          'manual': true,
          'validate': true,
          'namedInImReg': false,
          'licensedToProvideCare': false,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Administrative',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 23,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 24,
          'weight': 21,
          'prefix': '91',
          'manual': true,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Assessment',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 24,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 25,
          'weight': 1,
          'prefix': 'P1',
          'manual': false,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Full Pharmacist',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 2,
              'licenseCode': 25,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 26,
          'weight': 2,
          'prefix': 'P1',
          'manual': false,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Limited Pharmacist',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 2,
              'licenseCode': 26,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 27,
          'weight': 4,
          'prefix': 'P1',
          'manual': false,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Temporary Pharmacist',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 2,
              'licenseCode': 27,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 28,
          'weight': 3,
          'prefix': 'P1',
          'manual': false,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Student Pharmacist',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 2,
              'licenseCode': 28,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 29,
          'weight': 6,
          'prefix': 'T9',
          'manual': false,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Pharmacy Technician',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 2,
              'licenseCode': 29,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 30,
          'weight': 5,
          'prefix': 'P1',
          'manual': true,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': false,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Non-Practicing Pharmacist',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 2,
              'licenseCode': 30,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 31,
          'weight': 7,
          'prefix': 'T9',
          'manual': true,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': false,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Non-Practicing Pharmacy Technician',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 2,
              'licenseCode': 31,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 32,
          'weight': 6,
          'prefix': 'R9',
          'manual': false,
          'validate': true,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': 1,
          'name': 'Practicing Registered Nurse',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 32,
              'collegeLicenseGroupingCode': 2,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 33,
          'weight': 7,
          'prefix': 'R9',
          'manual': false,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Provisional Registered Nurse',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 33,
              'collegeLicenseGroupingCode': 2,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 34,
          'weight': 10,
          'prefix': 'R9',
          'manual': true,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': false,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Non-Practicing Registered Nurse',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 34,
              'collegeLicenseGroupingCode': 2,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 35,
          'weight': 12,
          'prefix': 'R9',
          'manual': true,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': 1,
          'name': 'Practicing Licensed Graduate Nurse',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 35,
              'collegeLicenseGroupingCode': 2,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 36,
          'weight': 13,
          'prefix': 'R9',
          'manual': true,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Provisional Licensed Graduate Nurse',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 36,
              'collegeLicenseGroupingCode': 2,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 37,
          'weight': 14,
          'prefix': 'R9',
          'manual': false,
          'validate': true,
          'namedInImReg': false,
          'licensedToProvideCare': false,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Non-Practicing Licensed Graduate Nurse',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 37,
              'collegeLicenseGroupingCode': 2,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 39,
          'weight': 9,
          'prefix': 'R9',
          'manual': false,
          'validate': true,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': 1,
          'name': 'Temporary Registered Nurse (Emergency)',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 39,
              'collegeLicenseGroupingCode': 2,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 40,
          'weight': 11,
          'prefix': 'R9',
          'manual': false,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Employed Student Nurse',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 40,
              'collegeLicenseGroupingCode': 2,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 41,
          'weight': 15,
          'prefix': 'Y9',
          'manual': false,
          'validate': true,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': 1,
          'name': 'Practicing Registered Psychiatric Nurse',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 41,
              'collegeLicenseGroupingCode': 3,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 42,
          'weight': 16,
          'prefix': 'Y9',
          'manual': false,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Provisional Registered Psychiatric Nurse',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 42,
              'collegeLicenseGroupingCode': 3,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 43,
          'weight': 19,
          'prefix': 'Y9',
          'manual': true,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': false,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Non-Practicing Registered Psychiatric Nurse',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 43,
              'collegeLicenseGroupingCode': 3,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 45,
          'weight': 18,
          'prefix': 'Y9',
          'manual': false,
          'validate': true,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': 1,
          'name': 'Temporary Registered Psychiatric Nurse (Emergency)',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 45,
              'collegeLicenseGroupingCode': 3,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 46,
          'weight': 20,
          'prefix': 'Y9',
          'manual': false,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Employed Student Psychiatric Nurse',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 46,
              'collegeLicenseGroupingCode': 3,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 47,
          'weight': 1,
          'prefix': '96',
          'manual': false,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': 2,
          'name': 'Practicing Nurse Practitioner',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 47,
              'collegeLicenseGroupingCode': 4,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 48,
          'weight': 2,
          'prefix': '96',
          'manual': true,
          'validate': false,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': null,
          'name': 'Provisional Nurse Practitioner',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 48,
              'collegeLicenseGroupingCode': 4,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 49,
          'weight': 5,
          'prefix': '96',
          'manual': true,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': false,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Non-Practicing Nurse Practitioner',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 49,
              'collegeLicenseGroupingCode': 4,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 51,
          'weight': 4,
          'prefix': '96',
          'manual': false,
          'validate': true,
          'namedInImReg': true,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': true,
          'prescriberIdType': 2,
          'name': 'Temporary Nurse Practitioner (Emergency)',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 51,
              'collegeLicenseGroupingCode': 4,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 52,
          'weight': 21,
          'prefix': 'L9',
          'manual': false,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Practicing Licensed Practical Nurse',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 52,
              'collegeLicenseGroupingCode': 1,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 53,
          'weight': 22,
          'prefix': 'L9',
          'manual': false,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Provisional Licensed Practical Nurse',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 53,
              'collegeLicenseGroupingCode': 1,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 54,
          'weight': 25,
          'prefix': 'L9',
          'manual': true,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': false,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Non-Practicing Licensed Practical Nurse',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 54,
              'collegeLicenseGroupingCode': 1,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 55,
          'weight': 23,
          'prefix': 'L9',
          'manual': false,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Temporary Licensed Practical Nurse (Emergency)',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 55,
              'collegeLicenseGroupingCode': 1,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 59,
          'weight': 25,
          'prefix': '93',
          'manual': true,
          'validate': true,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Podiatric Surgeon',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 59,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 60,
          'weight': 28,
          'prefix': '98',
          'manual': false,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Practising Midwife',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 60,
              'collegeLicenseGroupingCode': 5,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 61,
          'weight': 29,
          'prefix': '98',
          'manual': false,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Provisional Midwife',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 61,
              'collegeLicenseGroupingCode': 5,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 62,
          'weight': 30,
          'prefix': '98',
          'manual': false,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Temporary Midwife (Emergency)',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 62,
              'collegeLicenseGroupingCode': 5,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 63,
          'weight': 31,
          'prefix': '98',
          'manual': true,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Non-Practising Midwife',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 63,
              'collegeLicenseGroupingCode': 5,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 64,
          'weight': 1,
          'prefix': '',
          'manual': true,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': false,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Not Displayed',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 4,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 5,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 6,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 7,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 8,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 9,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 10,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 11,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 12,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 13,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 14,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 15,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 16,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 17,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            },
            {
              'collegeCode': 18,
              'licenseCode': 64,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 65,
          'weight': 26,
          'prefix': '93',
          'manual': true,
          'validate': true,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Educational - Podiatric Surgeon Student (Elective)',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 65,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 66,
          'weight': 27,
          'prefix': '93',
          'manual': true,
          'validate': true,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Educational - Podiatric Surgeon Resident (Elective)',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 66,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 67,
          'weight': 28,
          'prefix': '93',
          'manual': true,
          'validate': true,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Conditional - Podiatric Surgeon Disciplined',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 1,
              'licenseCode': 67,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 68,
          'weight': 8,
          'prefix': 'T9',
          'manual': true,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Temporary Pharmacy Technician',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 2,
              'licenseCode': 68,
              'collegeLicenseGroupingCode': null,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        },
        {
          'code': 69,
          'weight': 32,
          'prefix': '98',
          'manual': true,
          'validate': false,
          'namedInImReg': false,
          'licensedToProvideCare': true,
          'allowRequestRemoteAccess': false,
          'prescriberIdType': null,
          'name': 'Student Midwife',
          'multijurisdictional': false,
          'collegeLicenses': [
            {
              'collegeCode': 3,
              'licenseCode': 69,
              'collegeLicenseGroupingCode': 5,
              'discontinued': false
            }
          ],
          'remoteAccessTypeLicenses': null
        }
      ],
      'careSettings': [
        {
          'code': 1,
          'name': 'Health Authority'
        },
        {
          'code': 2,
          'name': 'Private Community Health Practice'
        },
        {
          'code': 3,
          'name': 'Community Pharmacy'
        },
        {
          'code': 4,
          'name': 'Device Provider'
        }
      ],
      'practices': [
        {
          'code': 1,
          'name': 'Remote Practice',
          'collegePractices': [
            {
              'collegeCode': 3,
              'practiceCode': 1
            }
          ]
        },
        {
          'code': 2,
          'name': 'Reproductive Health - Sexually Transmitted Infections',
          'collegePractices': [
            {
              'collegeCode': 3,
              'practiceCode': 2
            }
          ]
        },
        {
          'code': 3,
          'name': 'Reproductive Health - Contraceptive Management',
          'collegePractices': [
            {
              'collegeCode': 3,
              'practiceCode': 3
            }
          ]
        },
        {
          'code': 4,
          'name': 'First Call',
          'collegePractices': [
            {
              'collegeCode': 3,
              'practiceCode': 4
            }
          ]
        }
      ],
      'statuses': [
        {
          'code': 1,
          'name': 'Editable'
        },
        {
          'code': 2,
          'name': 'Under Review'
        },
        {
          'code': 3,
          'name': 'Requires TOA'
        },
        {
          'code': 4,
          'name': 'Locked'
        },
        {
          'code': 5,
          'name': 'Declined'
        }
      ],
      'countries': [
        {
          'code': 'US',
          'name': 'United States'
        },
        {
          'code': 'CA',
          'name': 'Canada'
        }
      ],
      'provinces': [
        {
          'code': 'WY',
          'countryCode': 'US',
          'name': 'Wyoming'
        },
        {
          'code': 'CA',
          'countryCode': 'US',
          'name': 'California'
        },
        {
          'code': 'CO',
          'countryCode': 'US',
          'name': 'Colorado'
        },
        {
          'code': 'CT',
          'countryCode': 'US',
          'name': 'Connecticut'
        },
        {
          'code': 'DE',
          'countryCode': 'US',
          'name': 'Delaware'
        },
        {
          'code': 'DC',
          'countryCode': 'US',
          'name': 'District of Columbia'
        },
        {
          'code': 'FL',
          'countryCode': 'US',
          'name': 'Florida'
        },
        {
          'code': 'GA',
          'countryCode': 'US',
          'name': 'Georgia'
        },
        {
          'code': 'GU',
          'countryCode': 'US',
          'name': 'Guam'
        },
        {
          'code': 'HI',
          'countryCode': 'US',
          'name': 'Hawaii'
        },
        {
          'code': 'ID',
          'countryCode': 'US',
          'name': 'Idaho'
        },
        {
          'code': 'IN',
          'countryCode': 'US',
          'name': 'Indiana'
        },
        {
          'code': 'IA',
          'countryCode': 'US',
          'name': 'Iowa'
        },
        {
          'code': 'KS',
          'countryCode': 'US',
          'name': 'Kansas'
        },
        {
          'code': 'AR',
          'countryCode': 'US',
          'name': 'Arkansas'
        },
        {
          'code': 'KY',
          'countryCode': 'US',
          'name': 'Kentucky'
        },
        {
          'code': 'AZ',
          'countryCode': 'US',
          'name': 'Arizona'
        },
        {
          'code': 'AK',
          'countryCode': 'US',
          'name': 'Alaska'
        },
        {
          'code': 'AB',
          'countryCode': 'CA',
          'name': 'Alberta'
        },
        {
          'code': 'BC',
          'countryCode': 'CA',
          'name': 'British Columbia'
        },
        {
          'code': 'MB',
          'countryCode': 'CA',
          'name': 'Manitoba'
        },
        {
          'code': 'NB',
          'countryCode': 'CA',
          'name': 'New Brunswick'
        },
        {
          'code': 'NL',
          'countryCode': 'CA',
          'name': 'Newfoundland and Labrador'
        },
        {
          'code': 'NS',
          'countryCode': 'CA',
          'name': 'Nova Scotia'
        },
        {
          'code': 'AS',
          'countryCode': 'US',
          'name': 'American Samoa'
        },
        {
          'code': 'ON',
          'countryCode': 'CA',
          'name': 'Ontario'
        },
        {
          'code': 'QC',
          'countryCode': 'CA',
          'name': 'Quebec'
        },
        {
          'code': 'SK',
          'countryCode': 'CA',
          'name': 'Saskatchewan'
        },
        {
          'code': 'NT',
          'countryCode': 'CA',
          'name': 'Northwest Territories'
        },
        {
          'code': 'NU',
          'countryCode': 'CA',
          'name': 'Nunavut'
        },
        {
          'code': 'YT',
          'countryCode': 'CA',
          'name': 'Yukon'
        },
        {
          'code': 'AL',
          'countryCode': 'US',
          'name': 'Alabama'
        },
        {
          'code': 'PE',
          'countryCode': 'CA',
          'name': 'Prince Edward Island'
        },
        {
          'code': 'LA',
          'countryCode': 'US',
          'name': 'Louisiana'
        },
        {
          'code': 'IL',
          'countryCode': 'US',
          'name': 'Illinois'
        },
        {
          'code': 'MD',
          'countryCode': 'US',
          'name': 'Maryland'
        },
        {
          'code': 'PR',
          'countryCode': 'US',
          'name': 'Puerto Rico'
        },
        {
          'code': 'RI',
          'countryCode': 'US',
          'name': 'Rhode Island'
        },
        {
          'code': 'SC',
          'countryCode': 'US',
          'name': 'South Carolina'
        },
        {
          'code': 'ME',
          'countryCode': 'US',
          'name': 'Maine'
        },
        {
          'code': 'TN',
          'countryCode': 'US',
          'name': 'Tennessee'
        },
        {
          'code': 'TX',
          'countryCode': 'US',
          'name': 'Texas'
        },
        {
          'code': 'PA',
          'countryCode': 'US',
          'name': 'Pennsylvania'
        },
        {
          'code': 'UM',
          'countryCode': 'US',
          'name': 'United States Minor Outlying Islands'
        },
        {
          'code': 'VT',
          'countryCode': 'US',
          'name': 'Vermont'
        },
        {
          'code': 'VI',
          'countryCode': 'US',
          'name': 'Virgin Islands, U.S.'
        },
        {
          'code': 'VA',
          'countryCode': 'US',
          'name': 'Virginia'
        },
        {
          'code': 'WA',
          'countryCode': 'US',
          'name': 'Washington'
        },
        {
          'code': 'WV',
          'countryCode': 'US',
          'name': 'West Virginia'
        },
        {
          'code': 'WI',
          'countryCode': 'US',
          'name': 'Wisconsin'
        },
        {
          'code': 'UT',
          'countryCode': 'US',
          'name': 'Utah'
        },
        {
          'code': 'OR',
          'countryCode': 'US',
          'name': 'Oregon'
        },
        {
          'code': 'SD',
          'countryCode': 'US',
          'name': 'South Dakota'
        },
        {
          'code': 'OH',
          'countryCode': 'US',
          'name': 'Ohio'
        },
        {
          'code': 'MA',
          'countryCode': 'US',
          'name': 'Massachusetts'
        },
        {
          'code': 'OK',
          'countryCode': 'US',
          'name': 'Oklahoma'
        },
        {
          'code': 'MN',
          'countryCode': 'US',
          'name': 'Minnesota'
        },
        {
          'code': 'MS',
          'countryCode': 'US',
          'name': 'Mississippi'
        },
        {
          'code': 'MO',
          'countryCode': 'US',
          'name': 'Missouri'
        },
        {
          'code': 'MT',
          'countryCode': 'US',
          'name': 'Montana'
        },
        {
          'code': 'NE',
          'countryCode': 'US',
          'name': 'Nebraska'
        },
        {
          'code': 'MI',
          'countryCode': 'US',
          'name': 'Michigan'
        },
        {
          'code': 'NH',
          'countryCode': 'US',
          'name': 'New Hampshire'
        },
        {
          'code': 'NJ',
          'countryCode': 'US',
          'name': 'New Jersey'
        },
        {
          'code': 'NM',
          'countryCode': 'US',
          'name': 'New Mexico'
        },
        {
          'code': 'NY',
          'countryCode': 'US',
          'name': 'New York'
        },
        {
          'code': 'NC',
          'countryCode': 'US',
          'name': 'North Carolina'
        },
        {
          'code': 'ND',
          'countryCode': 'US',
          'name': 'North Dakota'
        },
        {
          'code': 'MP',
          'countryCode': 'US',
          'name': 'Northern Mariana Islands'
        },
        {
          'code': 'NV',
          'countryCode': 'US',
          'name': 'Nevada'
        }
      ],
      'statusReasons': [
        {
          'code': 16,
          'name': 'Terms of Access to be determined by an Adjudicator'
        },
        {
          'code': 15,
          'name': 'User has Requested Remote Access'
        },
        {
          'code': 14,
          'name': 'User authenticated with a method other than BC Services Card'
        },
        {
          'code': 13,
          'name': 'User does not have high enough identity assurance level'
        },
        {
          'code': 12,
          'name': 'Admin has flagged the applicant for manual adjudication'
        },
        {
          'code': 11,
          'name': 'Contact Address or Identity Address not in British Columbia'
        },
        {
          'code': 10,
          'name': 'Answered one or more Self Declaration questions "Yes"'
        },
        {
          'code': 9,
          'name': 'Licence Class requires manual adjudication'
        },
        {
          'code': 5,
          'name': 'Name discrepancy in PharmaNet practitioner table'
        },
        {
          'code': 7,
          'name': 'Listed as Non-Practicing in PharmaNet practitioner table'
        },
        {
          'code': 6,
          'name': 'Birthdate discrepancy in PharmaNet practitioner table'
        },
        {
          'code': 4,
          'name': 'College License or Practitioner ID not in PharmaNet table'
        },
        {
          'code': 3,
          'name': 'PharmaNet Error, License could not be validated'
        },
        {
          'code': 2,
          'name': 'Manually Adjudicated'
        },
        {
          'code': 1,
          'name': 'Automatically Adjudicated'
        },
        {
          'code': 8,
          'name': 'Insulin Pump Provider'
        },
        {
          'code': 17,
          'name': 'No address from BCSC. Enrollee entered address.'
        },
        {
          'code': 18,
          'name': 'Manually entered paper enrolment'
        },
        {
          'code': 19,
          'name': 'PRIME enrolment does not match paper enrollee record'
        },
        {
          'code': 20,
          'name': 'Possible match with paper enrolment(s)'
        },
        {
          'code': 21,
          'name': 'Unable to link enrollee to paper enrolment'
        }
      ],
      'vendors': [
        {
          'code': 2,
          'careSettingCode': 2,
          'name': 'Excelleris'
        },
        {
          'code': 3,
          'careSettingCode': 2,
          'name': 'iClinic'
        },
        {
          'code': 4,
          'careSettingCode': 2,
          'name': 'Medinet'
        },
        {
          'code': 5,
          'careSettingCode': 2,
          'name': 'Plexia'
        },
        {
          'code': 6,
          'careSettingCode': 3,
          'name': 'PharmaClik'
        },
        {
          'code': 10,
          'careSettingCode': 3,
          'name': 'WinRx'
        },
        {
          'code': 8,
          'careSettingCode': 3,
          'name': 'Kroll'
        },
        {
          'code': 9,
          'careSettingCode': 3,
          'name': 'Assyst Rx-A'
        },
        {
          'code': 11,
          'careSettingCode': 3,
          'name': 'Shoppers Drug Mart HealthWatch NG'
        },
        {
          'code': 12,
          'careSettingCode': 3,
          'name': 'Commander Group'
        },
        {
          'code': 13,
          'careSettingCode': 3,
          'name': 'BDM'
        },
        {
          'code': 7,
          'careSettingCode': 3,
          'name': 'Nexxsys'
        },
        {
          'code': 1,
          'careSettingCode': 2,
          'name': 'CareConnect'
        },
        {
          'code': 14,
          'careSettingCode': 4,
          'name': 'Assyst Rx-A'
        },
        {
          'code': 15,
          'careSettingCode': 4,
          'name': 'Commander Group'
        },
        {
          'code': 16,
          'careSettingCode': 4,
          'name': 'Kroll'
        },
        {
          'code': 17,
          'careSettingCode': 4,
          'name': 'Nexxsys'
        },
        {
          'code': 18,
          'careSettingCode': 4,
          'name': 'PharmaClik'
        },
        {
          'code': 19,
          'careSettingCode': 4,
          'name': 'Shoppers Drug Mart HealthWatch NG'
        },
        {
          'code': 20,
          'careSettingCode': 4,
          'name': 'WinRx'
        },
        {
          'code': 21,
          'careSettingCode': 1,
          'name': 'CareConnect'
        },
        {
          'code': 22,
          'careSettingCode': 1,
          'name': 'Excelleris'
        },
        {
          'code': 23,
          'careSettingCode': 1,
          'name': 'iClinic'
        },
        {
          'code': 24,
          'careSettingCode': 1,
          'name': 'Medinet'
        },
        {
          'code': 25,
          'careSettingCode': 1,
          'name': 'Plexia'
        },
        {
          'code': 26,
          'careSettingCode': 1,
          'name': 'PharmaClik'
        },
        {
          'code': 27,
          'careSettingCode': 1,
          'name': 'Nexxsys'
        },
        {
          'code': 28,
          'careSettingCode': 1,
          'name': 'Kroll'
        },
        {
          'code': 29,
          'careSettingCode': 1,
          'name': 'Assyst Rx-A'
        },
        {
          'code': 30,
          'careSettingCode': 1,
          'name': 'WinRx'
        },
        {
          'code': 31,
          'careSettingCode': 1,
          'name': 'Shoppers Drug Mart HealthWatch NG'
        },
        {
          'code': 32,
          'careSettingCode': 1,
          'name': 'Commander Group'
        },
        {
          'code': 33,
          'careSettingCode': 1,
          'name': 'BDM'
        },
        {
          'code': 34,
          'careSettingCode': 1,
          'name': 'Meditech'
        },
        {
          'code': 35,
          'careSettingCode': 1,
          'name': 'Cerner'
        }
      ],
      'healthAuthorities': [
        {
          'code': 6,
          'name': 'Provincial Health Services Authority',
          'passcode': 'PHSA'
        },
        {
          'code': 4,
          'name': 'Island Health',
          'passcode': 'ISH'
        },
        {
          'code': 5,
          'name': 'Fraser Health',
          'passcode': 'FH'
        },
        {
          'code': 2,
          'name': 'Interior Health',
          'passcode': 'IH'
        },
        {
          'code': 1,
          'name': 'Northern Health',
          'passcode': 'NH'
        },
        {
          'code': 3,
          'name': 'Vancouver Coastal Health',
          'passcode': 'VCH'
        }
      ],
      'deviceProviderRoles': [
        {
          'deviceProviderRoleCode': 1,
          'code': 1,
          'name': 'Certified Prosthetist',
          'weight': 1,
          'certified': false
        },
        {
          'deviceProviderRoleCode': 2,
          'code': 2,
          'name': 'Certified Orthotist',
          'weight': 2,
          'certified': false
        },
        {
          'deviceProviderRoleCode': 3,
          'code': 3,
          'name': 'Certified Prosthetist Orthotist',
          'weight': 1,
          'certified': false
        },
        {
          'deviceProviderRoleCode': 4,
          'code': 4,
          'name': 'Registered Prosthetic Technician',
          'weight': 4,
          'certified': false
        },
        {
          'deviceProviderRoleCode': 5,
          'code': 5,
          'name': 'Registered Orthotic Technician',
          'weight': 5,
          'certified': false
        },
        {
          'deviceProviderRoleCode': 6,
          'code': 6,
          'name': 'Registered Prosthetic Orthotic Technician',
          'weight': 6,
          'certified': false
        },
        {
          'deviceProviderRoleCode': 7,
          'code': 7,
          'name': 'Orthotic Resident',
          'weight': 7,
          'certified': false
        },
        {
          'deviceProviderRoleCode': 8,
          'code': 8,
          'name': 'Prosthetic Resident',
          'weight': 8,
          'certified': false
        },
        {
          'deviceProviderRoleCode': 9,
          'code': 9,
          'name': 'Orthotic Intern',
          'weight': 9,
          'certified': false
        },
        {
          'deviceProviderRoleCode': 10,
          'code': 10,
          'name': 'Prosthetic Intern',
          'weight': 10,
          'certified': false
        },
        {
          'deviceProviderRoleCode': 11,
          'code': 11,
          'name': 'Compression Garment Fitter',
          'weight': 1,
          'certified': false
        },
        {
          'deviceProviderRoleCode': 12,
          'code': 12,
          'name': 'Breast prosthetic Fitter',
          'weight': 1,
          'certified': false
        },
        {
          'deviceProviderRoleCode': 13,
          'code': 13,
          'name': 'Ocularist',
          'weight': 13,
          'certified': false
        },
        {
          'deviceProviderRoleCode': 14,
          'code': 14,
          'name': 'Anaplastologist',
          'weight': 14,
          'certified': false
        },
        {
          'deviceProviderRoleCode': 5,
          'code': 15,
          'name': 'None',
          'weight': 15,
          'certified': false
        },
      ],
      'facilities': [
        {
          'code': 4,
          'name': 'Out-patient pharmacy'
        },
        {
          'code': 1,
          'name': 'Acute/ambulatory care'
        },
        {
          'code': 2,
          'name': 'Long-term care'
        },
        {
          'code': 3,
          'name': 'In-patient pharmacy'
        },
        {
          'code': 5,
          'name': 'Outpatient or community-based clinic'
        }
      ],
      'collegeLicenseGroupings': [
        {
          'code': 1,
          'name': 'Licensed Practical Nurse',
          'weight': 1
        },
        {
          'code': 2,
          'name': 'Registered Nurse/Licensed Graduate Nurse',
          'weight': 2
        },
        {
          'code': 3,
          'name': 'Registered Psychiatric Nurse',
          'weight': 3
        },
        {
          'code': 4,
          'name': 'Nurse Practitioner',
          'weight': 4
        },
        {
          'code': 5,
          'name': 'Midwife',
          'weight': 5
        }
      ],
      'careTypes': [
        {
          'code': 1,
          'name': 'Ambulatory Care'
        },
        {
          'code': 2,
          'name': 'Acute Care'
        }
      ],
      'securityGroups': [
        {
          'code': 1,
          'name': 'EMRMD (EMR - Community-based Clinics)'
        },
        {
          'code': 2,
          'name': 'HAD (Hospital Admitting)'
        },
        {
          'code': 3,
          'name': 'HAI (HA Viewer)'
        },
        {
          'code': 4,
          'name': 'HAP (Hospital Access)'
        },
        {
          'code': 5,
          'name': 'HNF (Emergency Department Access (EDAP))'
        },
        {
          'code': 6,
          'name': 'IP (In-patient Pharmacies - Hospital)'
        },
        {
          'code': 7,
          'name': 'MD (COMPAP)'
        },
        {
          'code': 8,
          'name': 'OP (Hospital Outpatient Pharmacy)'
        },
        {
          'code': 9,
          'name': 'VHA (Cerner Integration Site)'
        }
      ]
    };
    /* eslint-enable */
  }
}
