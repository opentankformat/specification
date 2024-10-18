﻿using OTF.SampleGenerator;
using otf23;
using otfSampleDataGenerator.utils;

namespace otfSampleDataGenerator.sampleClasses
{
	public static class otf23SampleClassGenerator
	{
		const MessageHeaderInfoOTFVersion OTFVersion = MessageHeaderInfoOTFVersion.OTF23;

		#region "Generator"
		public static void GenerateSampleMessages(string targetFolder)
		{
			//PreNotification Flow
			GenerateSampleMessages<Message_Container_PreNotification>(targetFolder, typeof(Message_Container_PreNotification).Name, sample_Message_Container_PreNotification(), GenerateDummyData<Message_Container_PreNotification>());
			GenerateSampleMessages<Message_TankContainer_PreNotification>(targetFolder, typeof(Message_TankContainer_PreNotification).Name, sample_Message_TankContainer_PreNotification(), GenerateDummyData<Message_TankContainer_PreNotification>());
			GenerateSampleMessages<Message_Container_PreNotification_StatusUpdate>(targetFolder, typeof(Message_Container_PreNotification_StatusUpdate).Name, sample_Message_Container_PreNotification_StatusUpdate(), GenerateDummyData<Message_Container_PreNotification_StatusUpdate>());
			GenerateSampleMessages<Message_TankContainer_PreNotification_StatusUpdate>(targetFolder, typeof(Message_TankContainer_PreNotification_StatusUpdate).Name, sample_Message_TankContainer_PreNotification_StatusUpdate(), GenerateDummyData<Message_TankContainer_PreNotification_StatusUpdate>());

			//Status Updates
			GenerateSampleMessages<Message_Container_StatusUpdate_Storage_Arrival>(targetFolder, typeof(Message_Container_StatusUpdate_Storage_Arrival).Name, sample_Message_Container_StatusUpdate_Storage_Arrival(), GenerateDummyData<Message_Container_StatusUpdate_Storage_Arrival>());
			GenerateSampleMessages<Message_TankContainer_StatusUpdate_Storage_Arrival>(targetFolder, typeof(Message_TankContainer_StatusUpdate_Storage_Arrival).Name, sample_Message_TankContainer_StatusUpdate_Storage_Arrival(), GenerateDummyData<Message_TankContainer_StatusUpdate_Storage_Arrival>());

			GenerateSampleMessages<Message_Container_StatusUpdate_Storage_Available>(targetFolder, typeof(Message_Container_StatusUpdate_Storage_Available).Name, sample_Message_Container_StatusUpdate_Storage_Available(), GenerateDummyData<Message_Container_StatusUpdate_Storage_Available>());
			GenerateSampleMessages<Message_TankContainer_StatusUpdate_Storage_Available>(targetFolder, typeof(Message_TankContainer_StatusUpdate_Storage_Available).Name, sample_Message_TankContainer_StatusUpdate_Storage_Available(), GenerateDummyData<Message_TankContainer_StatusUpdate_Storage_Available>());

			GenerateSampleMessages<Message_Container_StatusUpdate_Storage_Departure>(targetFolder, typeof(Message_Container_StatusUpdate_Storage_Departure).Name, sample_Message_Container_StatusUpdate_Storage_Departure(), GenerateDummyData<Message_Container_StatusUpdate_Storage_Departure>());
			GenerateSampleMessages<Message_TankContainer_StatusUpdate_Storage_Departure>(targetFolder, typeof(Message_TankContainer_StatusUpdate_Storage_Departure).Name, sample_Message_TankContainer_StatusUpdate_Storage_Departure(), GenerateDummyData<Message_TankContainer_StatusUpdate_Storage_Departure>());

			GenerateSampleMessages<Message_TankContainer_StatusUpdate_HeelDisposal>(targetFolder, typeof(Message_TankContainer_StatusUpdate_HeelDisposal).Name, sample_Message_TankContainer_StatusUpdate_HeelDisposal(), GenerateDummyData<Message_TankContainer_StatusUpdate_HeelDisposal>());
			GenerateSampleMessages<Message_TankContainer_StatusUpdate_Cleaning>(targetFolder, typeof(Message_TankContainer_StatusUpdate_Cleaning).Name, sample_Message_TankContainer_StatusUpdate_Cleaning(), GenerateDummyData<Message_TankContainer_StatusUpdate_Cleaning>());
			GenerateSampleMessages<Message_TankContainer_StatusUpdate_Heating>(targetFolder, typeof(Message_TankContainer_StatusUpdate_Heating).Name, sample_Message_TankContainer_StatusUpdate_Heating(), GenerateDummyData<Message_TankContainer_StatusUpdate_Heating>());
			GenerateSampleMessages<Message_TankContainer_StatusUpdate_Inspection>(targetFolder, typeof(Message_TankContainer_StatusUpdate_Inspection).Name, sample_Message_TankContainer_StatusUpdate_Inspection(), GenerateDummyData<Message_TankContainer_StatusUpdate_Inspection>());
			GenerateSampleMessages<Message_TankContainer_StatusUpdate_Transhipment>(targetFolder, typeof(Message_TankContainer_StatusUpdate_Transhipment).Name, sample_Message_TankContainer_StatusUpdate_Transhipment(), GenerateDummyData<Message_TankContainer_StatusUpdate_Transhipment>());

			//Workflow
			GenerateSampleMessages<Message_Container_Work_Estimate>(targetFolder, typeof(Message_Container_Work_Estimate).Name + "_Estimate", sample_Message_Container_Work_Estimate(), GenerateDummyData<Message_Container_Work_Estimate>());
			GenerateSampleMessages<Message_TankContainer_Work_Estimate>(targetFolder, typeof(Message_TankContainer_Work_Estimate).Name + "_Estimate", sample_Message_TankContainer_Work_Estimate(), GenerateDummyData<Message_TankContainer_Work_Estimate>());


            GenerateSampleMessages<Message_Container_Work_Estimate>(targetFolder, typeof(Message_Container_Work_Estimate).Name + "_Authorization", sample_Message_Container_Work_Estimate_Authorization(), GenerateDummyData<Message_Container_Work_Estimate>());
            GenerateSampleMessages<Message_TankContainer_Work_Estimate>(targetFolder, typeof(Message_TankContainer_Work_Estimate).Name + "_Authorization", sample_Message_TankContainer_Work_Estimate_Authorization(), GenerateDummyData<Message_TankContainer_Work_Estimate>());

            GenerateSampleMessages<Message_Container_StatusUpdate_Work>(targetFolder, typeof(Message_Container_StatusUpdate_Work).Name, sample_Message_Container_StatusUpdate_Work(), GenerateDummyData<Message_Container_StatusUpdate_Work>());
			GenerateSampleMessages<Message_TankContainer_StatusUpdate_Work>(targetFolder, typeof(Message_TankContainer_StatusUpdate_Work).Name, sample_Message_TankContainer_StatusUpdate_Work(), GenerateDummyData<Message_TankContainer_StatusUpdate_Work>());

			//Inspections
			GenerateSampleMessages<Message_TankContainer_InspectionReport>(targetFolder, typeof(Message_TankContainer_InspectionReport).Name, sample_Message_TankContainer_InspectionReport(), GenerateDummyData<Message_TankContainer_InspectionReport>());

		}
		static void GenerateSampleMessages<T>(string targetFolder, string fileName, object realworldMessage, object allfieldsMessage)
		{
			//Generate and immediately deserialize as a test
			DeserializeTestXml<T>(ExportSampleFileXml(realworldMessage, targetFolder, String.Format("sample_{0}_realworld", fileName)));
			DeserializeTestJson<T>(ExportSampleFileJson(realworldMessage, targetFolder, String.Format("sample_{0}_realworld", fileName)));

			DeserializeTestXml<T>(ExportSampleFileXml(allfieldsMessage, targetFolder, String.Format("sample_{0}_allfields", fileName)));
			DeserializeTestJson<T>(ExportSampleFileJson(allfieldsMessage, targetFolder, String.Format("sample_{0}_allfields", fileName)));
		}

		static T GenerateDummyData<T>() where T : new()
		{
			var o = DummyDataGenerator.GenerateDummyData<T>();
			return SetOTFDefaults(o);
		}

		static T SetOTFDefaults<T>(T instance)
		{
			var messageHeaderInfoProperty = typeof(T).GetProperty("MessageHeaderInfo");
			var messageHeader = messageHeaderInfoProperty.GetValue(instance) as MessageHeaderInfo;

			//Set OTF version
			var otfVersionProperty = typeof(MessageHeaderInfo).GetProperty("OTFVersion");
			otfVersionProperty.SetValue(messageHeader, OTFVersion);

			//Set OTF message type
			Enum.TryParse(typeof(T).Name, out MessageHeaderInfoOTFMessage mt);

			var otfMessageProperty = typeof(MessageHeaderInfo).GetProperty("OTFMessage");
			otfMessageProperty.SetValue(messageHeader, mt);

			return instance;
		}

		#endregion

		#region "Deserialization Tests"
		static void DeserializeTestXml<T>(string fileName)
		{
			var o = SerializationUtilsXml.DeserializeFromXml<T>(fileName);
			Type t = o.GetType();

			Console.WriteLine(string.Format("{0} successfully deserialized into {1}", fileName, t.Name));
		}

		static void DeserializeTestJson<T>(string fileName)
		{
			var o = SerializationUtilsJson.DeserializeFromJson<T>(fileName);
			Type t = o.GetType();

			Console.WriteLine(string.Format("{0} successfully deserialized into {1}", fileName, t.Name));
		}

		static string ExportSampleFileXml(object o, string folder, string filename)
		{
			//Prep target folder
			string targetfolder = string.Format("{0}\\{1}\\", folder, "xml");
			if (!Directory.Exists(targetfolder)) { Directory.CreateDirectory(targetfolder); }

			//Export
			string FullFileName = string.Format("{0}{1}", targetfolder, filename);
			SerializationUtilsXml.SerializeToXml(o, FullFileName);

			return FullFileName;
		}

		static string ExportSampleFileJson(object o, string folder, string filename)
		{
			//Prep target folder
			string targetfolder = string.Format("{0}\\{1}\\", folder, "json");
			if (!Directory.Exists(targetfolder)) { Directory.CreateDirectory(targetfolder); }

			//Export
			string FullFileName = string.Format("{0}{1}", targetfolder, filename);
			SerializationUtilsJson.SerializeToJson(o, FullFileName);

			return FullFileName;
		}

		#endregion

		#region "PreNotification"
		static Message_Container_PreNotification sample_Message_Container_PreNotification()
		{
			Message_Container_PreNotification m = new Message_Container_PreNotification
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_Container_PreNotification_MessageID,
					ConversationID = SampleData.MessageIdentifier_Container_PreNotification_ConversationID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_PreNotification_ByLessee),
					MessageType = MessageHeaderInfoMessageType.New,
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_Container_PreNotification,
					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						}
					},
				},
				ContainerPreNotificationType = Message_Container_PreNotificationContainerPreNotificationType.Redelivery,
				OrderInfo = new OrderInfo
				{
					SupplierInfo = new OrderInfoSupplierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						FacilityInfo = new FacilityInfo
                        {
							Name = SampleData.Depot_LocationID
						}
					},
					ClientInfo = new OrderInfoClientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						},
						OrderReference = SampleData.Lessor_OrderReference
					},					
				},
				ContainerInfo = new ContainerInfo
				{
					ContainerNumber = SampleData.ContainerNumber,
					ContainerType = SampleData.ContainerType
				},
				ArrivalInfo = new Message_Container_PreNotificationArrivalInfo
				{
					ETA = DateTime.Parse(SampleData.Arrival_ETA),
					RedeliveryReference = SampleData.Lessee_RedeliveryReference,
					Container_HaulierInfo = new Container_HaulierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
                        {
							ExternalID = SampleData.Arrival_HaulierName,
							Name = SampleData.Arrival_HaulierName
						},
						TruckDriverInfo = new Container_HaulierInfoTruckDriverInfo
                        {
							ContactInfo = new ContactInfo
                            {
								Name = SampleData.Arrival_TruckDriverName,
							}
                        },
						TruckLicensePlate = SampleData.Arrival_TruckLicensePlate,
					},
				},
				ServicesRequestInfo = new Message_Container_PreNotificationServicesRequestInfo
				{
					Container_ServiceRequest_Storage = new Container_ServiceRequest_Storage
					{
						Container_ServiceRequestInfo = new Container_ServiceRequestInfo
						{
							IsRequested = true,
						}
					},
					Container_ServiceRequest_Work_Repair = new Container_ServiceRequest_Work_Repair
					{
						LessorInfo = new Container_ServiceRequest_Work_RepairLessorInfo
						{
							BusinessUnitInfo = new BusinessUnitInfo
							{
								ExternalID = SampleData.Lessor_ID,
								Name = SampleData.Lessor_ID
							},
							Container_ServiceRequestInfo = new Container_ServiceRequestInfo
                            {
								 IsRequested = true,
								 AuthorizationReference = SampleData.Lessor_Work_RepairServiceRequest_AuthorizationReference,
							},
						},
						LesseeInfo = new  Container_ServiceRequest_Work_RepairLesseeInfo
						{
							BusinessUnitInfo = new BusinessUnitInfo
							{
								ExternalID = SampleData.Lessee_ID,
								Name = SampleData.Lessee_ID
							},
							Container_ServiceRequestInfo = new Container_ServiceRequestInfo
							{
								IsRequested = true,
								AuthorizationReference = SampleData.Lessee_Work_RepairServiceRequest_AuthorizationReference,
							},
						},
					}
				},
				DepartureInfo = new Message_Container_PreNotificationDepartureInfo
				{
					ReleaseReference = SampleData.Lessor_ReleaseReference,
					ContainerDestinationInfo = new Message_Container_PreNotificationDepartureInfoContainerDestinationInfo
                    {
						FacilityInfo = new FacilityInfo
                        {
							Name = SampleData.Departure_ContainerDestination,
						}
                    },
					Container_HaulierInfo = new Container_HaulierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Departure_HaulierName,
							Name = SampleData.Departure_HaulierName
						},
						TruckDriverInfo = new Container_HaulierInfoTruckDriverInfo
						{
							ContactInfo = new ContactInfo
							{
								Name = SampleData.Departure_TruckDriverName,
							}
						},
						TruckLicensePlate = SampleData.Departure_TruckLicensePlate,
					}
				},
				Remarks = new RemarksRemark[]
				{
					new RemarksRemark
					{
						ID = SampleData.Remark_PreNotification_ID,
						Date = DateTime.Parse(SampleData.Remark_PreNotification_Date),
						AuthorInfo = new RemarksRemarkAuthorInfo
						{
							 ContactInfo = new ContactInfo
							 {
								 Name = SampleData.Remark_PreNotification_Author,
							 }
						},
						Remark = SampleData.Remark_PreNotification
					}
				}

			};

			return m;
		}
		static Message_TankContainer_PreNotification sample_Message_TankContainer_PreNotification()
		{
			Message_TankContainer_PreNotification m = new Message_TankContainer_PreNotification
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_Container_PreNotification_MessageID,
					ConversationID = SampleData.MessageIdentifier_Container_PreNotification_ConversationID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_PreNotification_ByLessee),
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_TankContainer_PreNotification,
					MessageType = MessageHeaderInfoMessageType.New,

					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						}
					},
				},
				ContainerPreNotificationType = Message_Container_PreNotificationContainerPreNotificationType.Redelivery,
				OrderInfo = new OrderInfo
				{
					SupplierInfo = new OrderInfoSupplierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						FacilityInfo = new FacilityInfo
                        {
							Name = SampleData.Depot_LocationID
						}
					},
					ClientInfo = new OrderInfoClientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						},
						OrderReference = SampleData.Lessor_OrderReference
					},
				},
				TankContainerInfo = new TankContainerInfo
				{
					ContainerInfo = new ContainerInfo
					{
						ContainerNumber = SampleData.ContainerNumber,
						ContainerType = SampleData.ContainerType
					},
				},
				ArrivalInfo = new Message_TankContainer_PreNotificationArrivalInfo
				{
					ETA = DateTime.Parse(SampleData.Arrival_ETA),
					RedeliveryReference = SampleData.Lessee_RedeliveryReference,
					Container_HaulierInfo = new Container_HaulierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Arrival_HaulierName,
							Name = SampleData.Arrival_HaulierName
						},
						TruckDriverInfo = new Container_HaulierInfoTruckDriverInfo
						{
							ContactInfo = new ContactInfo
							{
								Name = SampleData.Arrival_TruckDriverName,
							}
						},
						TruckLicensePlate = SampleData.Arrival_TruckLicensePlate,
					},
					TankContainer_StateInfo = new TankContainer_StateInfo
					{
						IsLoaded = SampleData.Arrival_ContainerState_IsLoaded,
						FullLoadedAmount = (SampleData.Arrival_ContainerState_Compartment1_LoadedAmount + SampleData.Arrival_ContainerState_Compartment2_LoadedAmount),
						FullLoadedAmountUnit = SampleData.Arrival_ContainerState_LoadedAmountUnit,
						IsClean = false,
						IsPressurized = true,
						IsNitrogen = false,
						Compartments = new TankContainer_CompartmentsTankContainer_Compartment[]
						{
							new TankContainer_CompartmentsTankContainer_Compartment
							{
								CompartmentNumber = 1,
								CurrentProduct = new TankContainer_CompartmentsTankContainer_CompartmentCurrentProduct
								{
									ProductInfo = new ProductInfo
									{
										MainName = SampleData.Arrival_ContainerState_Compartment1_CurrentProductMainName,
										TradeName = SampleData.Arrival_ContainerState_Compartment1_CurrentProductTradeName
									},
									LoadedAmount = SampleData.Arrival_ContainerState_Compartment1_LoadedAmount,
									LoadedAmountUnit = SampleData.Arrival_ContainerState_LoadedAmountUnit
								},
								PreviousProduct1 = new TankContainer_CompartmentsTankContainer_CompartmentPreviousProduct1
								{
									ProductInfo = new ProductInfo
									{
										MainName = SampleData.Arrival_ContainerState_Compartment1_PreviousProduct1MainName,
										TradeName = SampleData.Arrival_ContainerState_Compartment1_PreviousProduct1TradeName
									}
								},
								PreviousProduct2 = new TankContainer_CompartmentsTankContainer_CompartmentPreviousProduct2
								{
									ProductInfo = new ProductInfo
									{
										MainName = SampleData.Arrival_ContainerState_Compartment1_PreviousProduct2MainName,
										TradeName = SampleData.Arrival_ContainerState_Compartment1_PreviousProduct2TradeName
									}
								},
								PreviousProduct3 = new TankContainer_CompartmentsTankContainer_CompartmentPreviousProduct3
								{
									ProductInfo = new ProductInfo
									{
										MainName = SampleData.Arrival_ContainerState_Compartment1_PreviousProduct3MainName,
										TradeName = SampleData.Arrival_ContainerState_Compartment1_PreviousProduct3TradeName
									}
								}
							},
							new TankContainer_CompartmentsTankContainer_Compartment
							{
								CompartmentNumber = 2,
								CurrentProduct = new TankContainer_CompartmentsTankContainer_CompartmentCurrentProduct
								{
									ProductInfo = new ProductInfo
									{
										MainName = SampleData.Arrival_ContainerState_Compartment2_CurrentProductMainName,
									},
									LoadedAmount = SampleData.Arrival_ContainerState_Compartment2_LoadedAmount,
									LoadedAmountUnit = SampleData.Arrival_ContainerState_LoadedAmountUnit
								},
								PreviousProduct1 = new TankContainer_CompartmentsTankContainer_CompartmentPreviousProduct1
								{
									ProductInfo = new ProductInfo
									{
										MainName = SampleData.Arrival_ContainerState_Compartment2_PreviousProduct1MainName,
										TradeName = SampleData.Arrival_ContainerState_Compartment2_PreviousProduct1TradeName
									}
								},
								PreviousProduct2 = new TankContainer_CompartmentsTankContainer_CompartmentPreviousProduct2
								{
									ProductInfo = new ProductInfo
									{
										MainName = SampleData.Arrival_ContainerState_Compartment2_PreviousProduct2MainName,
										TradeName = SampleData.Arrival_ContainerState_Compartment2_PreviousProduct2TradeName
									}
								},
								PreviousProduct3 = new TankContainer_CompartmentsTankContainer_CompartmentPreviousProduct3
								{
									ProductInfo = new ProductInfo
									{
										MainName = SampleData.Arrival_ContainerState_Compartment2_PreviousProduct3MainName,
										TradeName = SampleData.Arrival_ContainerState_Compartment2_PreviousProduct3TradeName
									}
								}
							},
						}
					}

				},
				ServicesRequestInfo = new Message_TankContainer_PreNotificationServicesRequestInfo
				{
					Container_ServiceRequest_Storage = new Container_ServiceRequest_Storage
					{
						Container_ServiceRequestInfo = new Container_ServiceRequestInfo
						{
							IsRequested = true,
						}
					},
					TankContainer_ServiceRequest_Cleaning = new TankContainer_ServiceRequest_Cleaning
					{
						Container_ServiceRequestInfo = new Container_ServiceRequestInfo
						{
							IsRequested = true,
							AuthorizationReference = SampleData.Lessee_CleaningServiceRequest_AuthorizationReference
						},
						RequestPolymerCleaning = SampleData.Lessee_CleaningServiceRequest_RequestPolymerCleaning,
						AdditionalCleaningActions = new TankContainer_CleaningActionsTankContainer_CleaningAction[]
						{
							new TankContainer_CleaningActionsTankContainer_CleaningAction
							{
								CompartmentNumber = SampleData.Lessee_CleaningServiceRequest_AdditionalCleaningAction1_CompartmentNumber,
								Quantity = SampleData.Lessee_CleaningServiceRequest_AdditionalCleaningAction1_Quantity,
								ActionID = SampleData.Lessee_CleaningServiceRequest_AdditionalCleaningAction1_Code
							}
						}

					},
					Container_ServiceRequest_Work_Repair = new Container_ServiceRequest_Work_Repair
					{
						LessorInfo = new Container_ServiceRequest_Work_RepairLessorInfo
						{
							BusinessUnitInfo = new BusinessUnitInfo
							{
								ExternalID = SampleData.Lessor_ID,
								Name = SampleData.Lessor_ID
							},
							Container_ServiceRequestInfo = new Container_ServiceRequestInfo
							{
								IsRequested = true,
								AuthorizationReference = SampleData.Lessor_Work_RepairServiceRequest_AuthorizationReference,
							},
						},
						LesseeInfo = new Container_ServiceRequest_Work_RepairLesseeInfo
						{
							BusinessUnitInfo = new BusinessUnitInfo
							{
								ExternalID = SampleData.Lessee_ID,
								Name = SampleData.Lessee_ID
							},
							Container_ServiceRequestInfo = new Container_ServiceRequestInfo
							{
								IsRequested = true,
								AuthorizationReference = SampleData.Lessee_Work_RepairServiceRequest_AuthorizationReference,
							},
						},
					}
				},
				DepartureInfo = new Message_TankContainer_PreNotificationDepartureInfo
				{
					ReleaseReference = SampleData.Lessor_ReleaseReference,
					ContainerDestinationInfo = new Message_TankContainer_PreNotificationDepartureInfoContainerDestinationInfo
                    {
						FacilityInfo = new FacilityInfo
                        {
							Name = SampleData.Departure_ContainerDestination,
						}
                    },

					Container_HaulierInfo = new Container_HaulierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Departure_HaulierName,
							Name = SampleData.Departure_HaulierName
						},
						TruckDriverInfo = new Container_HaulierInfoTruckDriverInfo
						{
							ContactInfo = new ContactInfo
							{
								Name = SampleData.Departure_TruckDriverName,
							}
						},
						TruckLicensePlate = SampleData.Departure_TruckLicensePlate,
					}
				},
				Remarks = new RemarksRemark[]
				{
					new RemarksRemark
					{					 
						ID = SampleData.Remark_PreNotification_ID,
						Date = DateTime.Parse(SampleData.Remark_PreNotification_Date),
						AuthorInfo = new RemarksRemarkAuthorInfo
						{
							 ContactInfo = new ContactInfo
                             {
								 Name = SampleData.Remark_PreNotification_Author,
							 }							
						},
						Remark = SampleData.Remark_PreNotification
					}
				}

			};

			return m;
		}

		static Message_Container_PreNotification_StatusUpdate sample_Message_Container_PreNotification_StatusUpdate()
		{
			Message_Container_PreNotification_StatusUpdate m = new Message_Container_PreNotification_StatusUpdate
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_Container_PreNotificationStatusUpdate_MessageID,
					ConversationID = SampleData.MessageIdentifier_Container_PreNotification_ConversationID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_PreNotification_StatusUpdate_ByDepot),
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_Container_PreNotification_StatusUpdate,
					MessageType = MessageHeaderInfoMessageType.New,

					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						}
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
				},
				OrderInfo = new OrderInfo
				{
					SupplierInfo = new OrderInfoSupplierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						FacilityInfo = new FacilityInfo
						{
							Name = SampleData.Depot_LocationID
						},
						ProformaReference = SampleData.Depot_ProformaReference,
					},					
					ClientInfo = new OrderInfoClientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						},
						OrderReference = SampleData.Lessor_OrderReference
					},											
				},
				ContainerInfo = new ContainerInfo
				{
					ContainerNumber = SampleData.ContainerNumber,
					ContainerType = SampleData.ContainerType
				},
				PreNotificationStatus = new Message_Container_PreNotification_StatusUpdatePreNotificationStatus
				{
					Status = Message_Container_PreNotification_StatusUpdatePreNotificationStatusStatus.Approved
				}
			};

			return m;
		}

		static Message_TankContainer_PreNotification_StatusUpdate sample_Message_TankContainer_PreNotification_StatusUpdate()
		{
			Message_TankContainer_PreNotification_StatusUpdate m = new Message_TankContainer_PreNotification_StatusUpdate
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_Container_PreNotificationStatusUpdate_MessageID,
					ConversationID = SampleData.MessageIdentifier_Container_PreNotification_ConversationID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_PreNotification_StatusUpdate_ByDepot),
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_TankContainer_PreNotification_StatusUpdate,
					MessageType = MessageHeaderInfoMessageType.New,

					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						ContactInfo = new ContactInfo
						{
							Name = SampleData.Lessee_ContactName
						},
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
				
				},
				OrderInfo = new OrderInfo
				{
					SupplierInfo = new OrderInfoSupplierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						FacilityInfo = new FacilityInfo
                        {
							Name = SampleData.Depot_LocationID,
						},
						ProformaReference = SampleData.Depot_ProformaReference,
					},
					
					ClientInfo = new OrderInfoClientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						},
						OrderReference = SampleData.Lessor_OrderReference
					},
					
				},
				TankContainerInfo = new TankContainerInfo
				{
					ContainerInfo = new ContainerInfo
					{
						ContainerNumber = SampleData.ContainerNumber,
						ContainerType = SampleData.ContainerType
					}
				},
				PreNotificationStatus = new Message_TankContainer_PreNotification_StatusUpdatePreNotificationStatus
				{
					Status = Message_Container_PreNotification_StatusUpdatePreNotificationStatusStatus.Approved
				}
			};

			return m;
		}
		#endregion

		#region "Status updates"
		static Message_Container_StatusUpdate_Storage_Arrival sample_Message_Container_StatusUpdate_Storage_Arrival()
		{
			Message_Container_StatusUpdate_Storage_Arrival m = new Message_Container_StatusUpdate_Storage_Arrival
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_Container_GateInStatusUpdate_MessageID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_GateIn_ByDepot),
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_Container_StatusUpdate_Storage_Arrival,
					MessageType = MessageHeaderInfoMessageType.New,

					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						ContactInfo = new ContactInfo
						{
							Name = SampleData.Lessee_ContactName
						},
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
				},
				OrderInfo = new OrderInfo
				{
					SupplierInfo = new OrderInfoSupplierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						FacilityInfo = new FacilityInfo
						{
							Name = SampleData.Depot_LocationID,
						},
						OrderReference = SampleData.Depot_OrderReference,
						OrderStatusInfo = new OrderStatusInfo
						{
							OrderStatus = SampleData.Orderflow_Container_StatusUpdate_GateInStatusUpdate_ByDepot_Status,
							OrderStatusDescription = SampleData.Orderflow_Container_StatusUpdate_GateInStatusUpdate_ByDepot_Status_Description
						},
					},
					ClientInfo = new OrderInfoClientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						},
						OrderReference = SampleData.Lessor_OrderReference
					},
				},
				ContainerInfo = new ContainerInfo
				{
					ContainerNumber = SampleData.ContainerNumber,
					ContainerType = SampleData.ContainerType
				},
				Container_StatusUpdate_Storage_Arrival = new Container_StatusUpdate_Storage_Arrival
				{
					ClientDeliveryReference = SampleData.Lessee_RedeliveryReference,
					ContainerDeliveryDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_GateIn_ByDepot)
				}
			};

			return m;
		}
		static Message_TankContainer_StatusUpdate_Storage_Arrival sample_Message_TankContainer_StatusUpdate_Storage_Arrival()
		{
			Message_TankContainer_StatusUpdate_Storage_Arrival m = new Message_TankContainer_StatusUpdate_Storage_Arrival
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_Container_GateInStatusUpdate_MessageID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_GateIn_ByDepot),
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_TankContainer_StatusUpdate_Storage_Arrival,
					MessageType = MessageHeaderInfoMessageType.New,

					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						ContactInfo = new ContactInfo
						{
							Name = SampleData.Lessee_ContactName
						},
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
				},
				OrderInfo = new OrderInfo
				{
					SupplierInfo = new OrderInfoSupplierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						FacilityInfo = new FacilityInfo
						{
							Name = SampleData.Depot_LocationID,
						},
						OrderReference = SampleData.Depot_OrderReference,
						OrderStatusInfo = new OrderStatusInfo
						{
							OrderStatus = SampleData.Orderflow_Container_StatusUpdate_GateInStatusUpdate_ByDepot_Status,
							OrderStatusDescription = SampleData.Orderflow_Container_StatusUpdate_GateInStatusUpdate_ByDepot_Status_Description
						},
					},
					ClientInfo = new OrderInfoClientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						},
						OrderReference = SampleData.Lessor_OrderReference
					},
				},
				TankContainerInfo = new TankContainerInfo
				{
					ContainerInfo = new ContainerInfo
					{
						ContainerNumber = SampleData.ContainerNumber,
						ContainerType = SampleData.ContainerType
					},
				},
				Container_StatusUpdate_Storage_Arrival = new Container_StatusUpdate_Storage_Arrival
				{
					ClientDeliveryReference = SampleData.Lessee_RedeliveryReference,
					ContainerDeliveryDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_GateIn_ByDepot)
				}
			};

			return m;
		}

		static Message_Container_StatusUpdate_Storage_Available sample_Message_Container_StatusUpdate_Storage_Available()
		{
			Message_Container_StatusUpdate_Storage_Available m = new Message_Container_StatusUpdate_Storage_Available
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_Container_AvailableStatusUpdate_MessageID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Available_ByDepot),
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_Container_StatusUpdate_Storage_Available,
					MessageType = MessageHeaderInfoMessageType.New,

					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						}
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
				},
				OrderInfo = new OrderInfo
				{
					SupplierInfo = new OrderInfoSupplierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						FacilityInfo = new FacilityInfo
						{
							Name = SampleData.Depot_LocationID,
						},
						OrderReference = SampleData.Depot_OrderReference,
						OrderStatusInfo = new OrderStatusInfo
						{
							OrderStatus = SampleData.Orderflow_Container_StatusUpdate_Available_ByDepot_Status,
							OrderStatusDescription = SampleData.Orderflow_Container_StatusUpdate_Available_ByDepot_Status_Description
						},

					},
					ClientInfo = new OrderInfoClientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						},
						OrderReference = SampleData.Lessor_OrderReference
					},
				},
				ContainerInfo = new ContainerInfo
				{
					ContainerNumber = SampleData.ContainerNumber,
					ContainerType = SampleData.ContainerType
				},
				Container_StatusUpdate_Storage_Available = new Container_StatusUpdate_Storage_Available
				{
					ContainerAvailableDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Available_ByDepot)
				}
			};
			return m;
		}
		static Message_TankContainer_StatusUpdate_Storage_Available sample_Message_TankContainer_StatusUpdate_Storage_Available()
		{
			Message_TankContainer_StatusUpdate_Storage_Available m = new Message_TankContainer_StatusUpdate_Storage_Available
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_Container_AvailableStatusUpdate_MessageID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Available_ByDepot),
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_TankContainer_StatusUpdate_Storage_Available,
					MessageType = MessageHeaderInfoMessageType.New,

					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						}
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
				},
				OrderInfo = new OrderInfo
				{
					SupplierInfo = new OrderInfoSupplierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						FacilityInfo = new FacilityInfo
                        {
							Name = SampleData.Depot_LocationID,
						},
						OrderReference = SampleData.Depot_OrderReference,
						OrderStatusInfo = new OrderStatusInfo
						{
							OrderStatus = SampleData.Orderflow_Container_StatusUpdate_Available_ByDepot_Status,
							OrderStatusDescription = SampleData.Orderflow_Container_StatusUpdate_Available_ByDepot_Status_Description
						},

					},
					ClientInfo = new OrderInfoClientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						},
						OrderReference = SampleData.Lessor_OrderReference
					},
				},
				TankContainerInfo = new TankContainerInfo
				{
					ContainerInfo = new ContainerInfo
					{
						ContainerNumber = SampleData.ContainerNumber,
						ContainerType = SampleData.ContainerType
					},
				},
				Container_StatusUpdate_Storage_Available = new Container_StatusUpdate_Storage_Available
				{
					ContainerAvailableDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Available_ByDepot)
				}
			};
			return m;
		}

		static Message_Container_StatusUpdate_Storage_Departure sample_Message_Container_StatusUpdate_Storage_Departure()
		{
			Message_Container_StatusUpdate_Storage_Departure m = new Message_Container_StatusUpdate_Storage_Departure
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_Container_GateOutStatusUpdate_MessageID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_GateOut_ByDepot),
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_Container_StatusUpdate_Storage_Departure,
					MessageType = MessageHeaderInfoMessageType.New,

					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						}
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
				},
				OrderInfo = new OrderInfo
				{
					SupplierInfo = new OrderInfoSupplierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						FacilityInfo = new FacilityInfo
						{
							Name = SampleData.Depot_LocationID,
						},
						OrderReference = SampleData.Depot_OrderReference,
						OrderStatusInfo = new OrderStatusInfo
						{
							OrderStatus = SampleData.Orderflow_Container_StatusUpdate_GateOut_ByDepot_Status,
							OrderStatusDescription = SampleData.Orderflow_Container_StatusUpdate_GateOut_ByDepot_Status_Description
						},
					},
					ClientInfo = new OrderInfoClientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						},
						OrderReference = SampleData.Lessor_OrderReference
					},
				},
				ContainerInfo = new ContainerInfo
				{
					ContainerNumber = SampleData.ContainerNumber,
					ContainerType = SampleData.ContainerType
				},
				Container_StatusUpdate_Storage_Departure = new Container_StatusUpdate_Storage_Departure
				{
					ClientReleaseReference = SampleData.Lessor_ReleaseReference,
					ContainerReleaseDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_GateOut_ByDepot)
				}
			};
			return m;
		}
		static Message_TankContainer_StatusUpdate_Storage_Departure sample_Message_TankContainer_StatusUpdate_Storage_Departure()
		{
			Message_TankContainer_StatusUpdate_Storage_Departure m = new Message_TankContainer_StatusUpdate_Storage_Departure
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_Container_GateOutStatusUpdate_MessageID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_GateOut_ByDepot),
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_TankContainer_StatusUpdate_Storage_Departure,
					MessageType = MessageHeaderInfoMessageType.New,

					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						}
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
				},
				OrderInfo = new OrderInfo
				{
					SupplierInfo = new OrderInfoSupplierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						FacilityInfo = new FacilityInfo
						{
							Name = SampleData.Depot_LocationID,
						},
						OrderReference = SampleData.Depot_OrderReference,
						OrderStatusInfo = new OrderStatusInfo
						{
							OrderStatus = SampleData.Orderflow_Container_StatusUpdate_GateOut_ByDepot_Status,
							OrderStatusDescription = SampleData.Orderflow_Container_StatusUpdate_GateOut_ByDepot_Status_Description
						},
					},
					ClientInfo = new OrderInfoClientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						},
						OrderReference = SampleData.Lessor_OrderReference
					},
				},
				TankContainerInfo = new TankContainerInfo
				{
					ContainerInfo = new ContainerInfo
					{
						ContainerNumber = SampleData.ContainerNumber,
						ContainerType = SampleData.ContainerType
					}
				},
				Container_StatusUpdate_Storage_Departure = new Container_StatusUpdate_Storage_Departure
				{
					ClientReleaseReference = SampleData.Lessor_ReleaseReference,
					ContainerReleaseDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_GateOut_ByDepot)
				}
			};
			return m;
		}

		static Message_TankContainer_StatusUpdate_HeelDisposal sample_Message_TankContainer_StatusUpdate_HeelDisposal()
		{
			Message_TankContainer_StatusUpdate_HeelDisposal m = new Message_TankContainer_StatusUpdate_HeelDisposal
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_TankContainer_HeelDisposalStatusUpdate_MessageID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_HeelDisposal_ByDepot_Finished),
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_TankContainer_StatusUpdate_HeelDisposal,
					MessageType = MessageHeaderInfoMessageType.New,

					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						ContactInfo = new ContactInfo
						{
							Name = SampleData.Lessee_ContactName
						},
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
				},
				OrderInfo = new OrderInfo
				{
					SupplierInfo = new OrderInfoSupplierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						FacilityInfo = new FacilityInfo
						{
							Name = SampleData.Depot_LocationID,
						},
						OrderReference = SampleData.Depot_OrderReference,
						OrderStatusInfo = new OrderStatusInfo
						{
							OrderStatus = SampleData.Orderflow_Container_StatusUpdate_HeelDisposal_ByDepot_Status,
							OrderStatusDescription = SampleData.Orderflow_Container_StatusUpdate_HeelDisposal_ByDepot_Status_Description
						},
					},
					ClientInfo = new OrderInfoClientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						},
						OrderReference = SampleData.Lessor_OrderReference
					},
				},
				TankContainerInfo = new TankContainerInfo
				{
					ContainerInfo = new ContainerInfo
					{
						ContainerNumber = SampleData.ContainerNumber,
						ContainerType = SampleData.ContainerType
					},
				},
				Container_ServiceProgressInfo = new Container_ServiceProgressInfo
				{
					SupplierReference = SampleData.Depot_OrderReference_HeelDisposal,
					Container_ServiceProgress_DateInfo = new Container_ServiceProgress_DateInfo
					{
						Started = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_HeelDisposal_ByDepot_Started),
						Finished = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_HeelDisposal_ByDepot_Finished)
					}
				},
			};
			return m;
		}

		static Message_TankContainer_StatusUpdate_Cleaning sample_Message_TankContainer_StatusUpdate_Cleaning()
		{
			Message_TankContainer_StatusUpdate_Cleaning m = new Message_TankContainer_StatusUpdate_Cleaning
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_TankContainer_CleaningStatusUpdate_MessageID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Cleaning_ByDepot_Finished),
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_TankContainer_StatusUpdate_Cleaning,
					MessageType = MessageHeaderInfoMessageType.New,

					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						ContactInfo = new ContactInfo
						{
							Name = SampleData.Lessee_ContactName
						},
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
				},
				OrderInfo = new OrderInfo
				{
					SupplierInfo = new OrderInfoSupplierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						FacilityInfo = new FacilityInfo
						{
							Name = SampleData.Depot_LocationID,
						},
						OrderReference = SampleData.Depot_OrderReference,
						OrderStatusInfo = new OrderStatusInfo
						{
							OrderStatus = SampleData.Orderflow_Container_StatusUpdate_Cleaning_ByDepot_Status,
							OrderStatusDescription = SampleData.Orderflow_Container_StatusUpdate_Cleaning_ByDepot_Status_Description
						},
					},
					ClientInfo = new OrderInfoClientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						},
						OrderReference = SampleData.Lessor_OrderReference
					},
				},
				TankContainerInfo = new TankContainerInfo
				{
					ContainerInfo = new ContainerInfo
					{
						ContainerNumber = SampleData.ContainerNumber,
						ContainerType = SampleData.ContainerType
					},
				},
				Container_ServiceProgressInfo = new Container_ServiceProgressInfo
				{
					SupplierReference = SampleData.Depot_OrderReference_Cleaning,
					Container_ServiceProgress_AuthorizationInfo = new Container_ServiceProgress_AuthorizationInfo
					{
						AuthorizationReference = SampleData.Lessee_CleaningServiceRequest_AuthorizationReference
					},
					Container_ServiceProgress_DateInfo = new Container_ServiceProgress_DateInfo
                    {
						Started = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Cleaning_ByDepot_Started),
						Finished = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Cleaning_ByDepot_Finished)
					}
				},
				TankContainerCleaningInfo = new Message_TankContainer_StatusUpdate_CleaningTankContainerCleaningInfo
				{
					CleaningCertificateID = SampleData.Orderflow_Container_StatusUpdate_Cleaning_ByDepot_CleaningCertificateID
				}
			};
			return m;
		}


		static Message_TankContainer_StatusUpdate_Heating sample_Message_TankContainer_StatusUpdate_Heating()
		{
			Message_TankContainer_StatusUpdate_Heating m = new Message_TankContainer_StatusUpdate_Heating
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_TankContainer_HeatingStatusUpdate_MessageID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Heating_ByDepot_Started),
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_TankContainer_StatusUpdate_Heating,
					MessageType = MessageHeaderInfoMessageType.New,

					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						ContactInfo = new ContactInfo
						{
							Name = SampleData.Lessee_ContactName
						},
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
				},
				OrderInfo = new OrderInfo
				{
					SupplierInfo = new OrderInfoSupplierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						FacilityInfo = new FacilityInfo
                        {
							Name = SampleData.Depot_LocationID,
						},
						OrderReference = SampleData.Depot_OrderReference,
						OrderStatusInfo = new OrderStatusInfo
						{
							OrderStatusDescription = SampleData.Orderflow_Container_StatusUpdate_Heating_ByDepot_StatusDescription
						},
					},
					ClientInfo = new OrderInfoClientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						},
						OrderReference = SampleData.Lessor_OrderReference
					},
				},
				TankContainerInfo = new TankContainerInfo
				{

					ContainerInfo = new ContainerInfo
					{
						ContainerNumber = SampleData.ContainerNumber,
						ContainerType = SampleData.ContainerType
					},
				},
				Container_ServiceProgressInfo = new Container_ServiceProgressInfo
				{
					SupplierReference = SampleData.Depot_OrderReference_Heating,
					Container_ServiceProgress_DateInfo = new Container_ServiceProgress_DateInfo
					{
						Started = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Heating_ByDepot_Started),
					}
				},
			};
			return m;
		}

		static Message_TankContainer_StatusUpdate_Inspection sample_Message_TankContainer_StatusUpdate_Inspection()
		{
			Message_TankContainer_StatusUpdate_Inspection m = new Message_TankContainer_StatusUpdate_Inspection
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_TankContainer_InspectionStatusUpdate_MessageID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Inspection_ByDepot_Started),
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_TankContainer_StatusUpdate_Inspection,
					MessageType = MessageHeaderInfoMessageType.New,

					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						}
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
				},
				OrderInfo = new OrderInfo
				{
					SupplierInfo = new OrderInfoSupplierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						FacilityInfo = new FacilityInfo
						{
							Name = SampleData.Depot_LocationID,
						},
						OrderReference = SampleData.Depot_OrderReference,
						OrderStatusInfo = new OrderStatusInfo
						{
							OrderStatusDescription = SampleData.Orderflow_Container_StatusUpdate_Inspection_ByDepot_Status_Description
						},
					},
					ClientInfo = new OrderInfoClientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						},
						OrderReference = SampleData.Lessor_OrderReference
					},
				},
				TankContainerInfo = new TankContainerInfo
				{

					ContainerInfo = new ContainerInfo
					{
						ContainerNumber = SampleData.ContainerNumber,
						ContainerType = SampleData.ContainerType
					},
				},
				Container_ServiceProgressInfo = new Container_ServiceProgressInfo
				{
					SupplierReference = SampleData.Depot_OrderReference_Inspection,
					Container_ServiceProgress_DateInfo = new Container_ServiceProgress_DateInfo
					{
						Started = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Inspection_ByDepot_Started),
					}
				}
			};
			return m;
		}

		static Message_TankContainer_StatusUpdate_Transhipment sample_Message_TankContainer_StatusUpdate_Transhipment()
		{
			Message_TankContainer_StatusUpdate_Transhipment m = new Message_TankContainer_StatusUpdate_Transhipment
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_TankContainer_TranshipmentStatusUpdate_MessageID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Transhipment_ByDepot_Finished),
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_TankContainer_StatusUpdate_Transhipment,
					MessageType = MessageHeaderInfoMessageType.New,

					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						ContactInfo = new ContactInfo
						{
							Name = SampleData.Lessee_ContactName
						},
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
				},
				OrderInfo = new OrderInfo
				{
					SupplierInfo = new OrderInfoSupplierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						FacilityInfo = new FacilityInfo
						{
							Name = SampleData.Depot_LocationID,
						},
						OrderReference = SampleData.Depot_OrderReference,
						OrderStatusInfo = new OrderStatusInfo
						{
							OrderStatus = SampleData.Orderflow_Container_StatusUpdate_Transhipment_ByDepot_Status,
							OrderStatusDescription = SampleData.Orderflow_Container_StatusUpdate_Transhipment_ByDepot_Status_Description
						},
					},
					ClientInfo = new OrderInfoClientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						},
						OrderReference = SampleData.Lessor_OrderReference
					},
				},
				TankContainerInfo = new TankContainerInfo
				{
					ContainerInfo = new ContainerInfo
					{
						ContainerNumber = SampleData.ContainerNumber,
						ContainerType = SampleData.ContainerType
					},
				},
				Container_ServiceProgressInfo = new Container_ServiceProgressInfo
				{
					SupplierReference = SampleData.Depot_OrderReference_Transhipment,
					Container_ServiceProgress_DateInfo = new Container_ServiceProgress_DateInfo
					{
						Planned = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Transhipment_ByDepot_Planned),
						PlannedToTakePlaceOn = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Transhipment_ByDepot_PlannedOn),
						Started = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Transhipment_ByDepot_Started),
						Finished = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Transhipment_ByDepot_Finished)
					}
				}
			};
			return m;
		}
		#endregion

		#region "Work flow"

		static Message_Container_Work_Estimate sample_Message_Container_Work_Estimate()
		{
			Message_Container_Work_Estimate m = new Message_Container_Work_Estimate
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_Container_Work_Repair_Estimate_MessageID,
					ConversationID = SampleData.MessageIdentifier_Container_Work_Repair_ConversationID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Work_ByDepot_Estimate_Finished),
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_Container_Work_Estimate,
					MessageType = MessageHeaderInfoMessageType.New,

					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						}
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
				},
				OrderInfo = new OrderInfo
				{
					SupplierInfo = new OrderInfoSupplierInfo
                    {
						BusinessUnitInfo = new BusinessUnitInfo
                        {
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						FacilityInfo = new FacilityInfo
						{
							Name = SampleData.Depot_LocationID,
						},
						OrderReference = SampleData.Depot_OrderReference,
						OrderStatusInfo = new OrderStatusInfo
						{
							OrderStatus = SampleData.Orderflow_Container_StatusUpdate_Work_Estimate_ByDepot_Status,
							OrderStatusDescription = SampleData.Orderflow_Container_StatusUpdate_Work_Estimate_ByDepot_Status_Description
						},
					},
					ClientInfo = new OrderInfoClientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						},
						OrderReference = SampleData.Lessor_OrderReference
					},
				},
				LocalizationInfo = new LocalizationInfo()
				{
					CurrencyISO = SampleData.Depot_Localization_CurrencyISO,
					ExchangeRate = SampleData.Depot_Localization_CurrencyExchangeRate,
				},

				ContainerInfo = new ContainerInfo
				{
					ContainerNumber = SampleData.ContainerNumber,
					ContainerType = SampleData.ContainerType
				},

				Container_Work_EstimateStatusInfo = new Container_Work_EstimateStatusInfo
				{
					ContainerWorkStatus = Container_Work_EstimateStatusInfoContainerWorkStatus.Estimate,
					Version = SampleData.Orderflow_Container_Work_Repair_ByDepot_Version,
					SupplierInfo = new Container_Work_EstimateStatusInfoSupplierInfo
                    {
						EstimatorInfo = new Container_Work_EstimateStatusInfoSupplierInfoEstimatorInfo
						{
							ContactInfo = new ContactInfo
							{
								Name = SampleData.Orderflow_Container_Work_Repair_ByDepot_EstimatorName,
							}
						},
						Container_ServiceProgress_DateInfo = new Container_ServiceProgress_DateInfo
						{
							Finished = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Work_ByDepot_Estimate_Finished)
						},
					},
					LessorInfo = new Container_Work_EstimateStatusInfoLessorInfo
                    {
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID,
						}
					},
				},
				ContainerStorageArrivalInfo = new Message_Container_Work_EstimateContainerStorageArrivalInfo
				{
					ContainerDeliveryDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_GateIn_ByDepot)
				},
				LaborRate = SampleData.Depot_Pricing_MandR_LaborRate,
				EstimateLines = new Container_Work_EstimateLinesContainer_Work_EstimateLine[]
				{
					 new Container_Work_EstimateLinesContainer_Work_EstimateLine
					 {
						 LineID = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MandREstimateLineID,
						 OriginID = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MandREstimateLineOriginID,
						 SortOrder = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_SortOrder,
						 Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_Description,
						 CategoryCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineCategoryCode
						 {
							  Code = new Code
							  {
								  Value = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_CategoryCode,
								  Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_CategoryCodeDescription
							  }
						 },
						 GeneralCodes = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodes
						 {
							  DamageCode =  new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesDamageCode
							  {
								  Code = new Code
								  {
									Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_DamageCode,
									Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_DamageCodeDescription
								  }
							  },
							  LocationCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesLocationCode
							  {
								  Code = new Code
								  {
									Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LocationCode,
									Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LocationCodeDescription
								  }

							  },
							  RepairCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesRepairCode
							  {
								  Code = new Code
								  {
									Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_RepairCode,
									Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_RepairCodeDescription
								  }

							  }
						 },
						 Proposal = new Container_Work_EstimateLinesContainer_Work_EstimateLineProposal
						 {

							  Container_Work_EstimateLineProposal = new Container_Work_EstimateLineProposal
							  {
								  LaborRate = SampleData.Depot_Pricing_MandR_LaborRate,
								  LaborHours = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LaborHours,
								  LaborPrice = (SampleData.Depot_Pricing_MandR_LaborRate*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LaborHours),
								  MaterialPrice = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MaterialPrice,
								  LineSubtotal = ((SampleData.Depot_Pricing_MandR_LaborRate*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LaborHours)+SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MaterialPrice),
								  Quantity = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_Quantity,
								  LineTotal = ((SampleData.Depot_Pricing_MandR_LaborRate*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LaborHours)+SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MaterialPrice)*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_Quantity,
								  DoAction = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_DoAction,
							  }
						 }
					 },
					 new Container_Work_EstimateLinesContainer_Work_EstimateLine
					 {
						 LineID = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_MandREstimateLineID,
						 OriginID = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_MandREstimateLineOriginID,
						 SortOrder = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_SortOrder,
						 Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_Description,
						 CategoryCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineCategoryCode
						 {
							  Code = new Code
							  {
								  Value = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_CategoryCode,
								  Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_CategoryCodeDescription
							  }
						 },
						 GeneralCodes = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodes
						 {
							  DamageCode =  new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesDamageCode
							  {
								  Code = new Code
								  {
									Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_DamageCode,
									Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_DamageCodeDescription
								  }
							  },
							  LocationCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesLocationCode
							  {
								  Code = new Code
								  {
									Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LocationCode,
									Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LocationCodeDescription
								  }

							  },
							  RepairCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesRepairCode
							  {
								  Code = new Code
								  {
									Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_RepairCode,
									Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_RepairCodeDescription
								  }

							  }
						 },
						 Proposal = new Container_Work_EstimateLinesContainer_Work_EstimateLineProposal
						 {
							  Container_Work_EstimateLineProposal = new Container_Work_EstimateLineProposal
							  {
								  LaborRate = SampleData.Depot_Pricing_MandR_LaborRate,
								  LaborHours = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LaborHours,
								  LaborPrice = (SampleData.Depot_Pricing_MandR_LaborRate*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LaborHours),
								  MaterialPrice = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_MaterialPrice,
								  LumpSumPrice = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LumpSumPrice,
								  LineSubtotal = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LumpSumPrice,
								  Quantity = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_Quantity,
								  LineTotal = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LumpSumPrice*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_Quantity,
								  DoAction = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_DoAction,
							  }
						 }
					}
				},
				Remarks = new RemarksRemark[]
				{
					new RemarksRemark
					{
						ID = SampleData.Remark_Work_Repair_ID,
						Date = DateTime.Parse(SampleData.Remark_Work_Repair_Date),
						AuthorInfo = new RemarksRemarkAuthorInfo
						{
							 ContactInfo = new ContactInfo
							 {
								 Name = SampleData.Remark_Work_Repair_Author,
							 }
						},
						Remark = SampleData.Remark_Work_Repair
					}
				}

			};
			return m;
		}
		static Message_TankContainer_Work_Estimate sample_Message_TankContainer_Work_Estimate()
		{
			Message_TankContainer_Work_Estimate m = new Message_TankContainer_Work_Estimate
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_Container_Work_Repair_Estimate_MessageID,
					ConversationID = SampleData.MessageIdentifier_Container_Work_Repair_ConversationID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Work_ByDepot_Estimate_Finished),
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_TankContainer_Work_Estimate,
					MessageType = MessageHeaderInfoMessageType.New,

					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						}
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
				},
				OrderInfo = new OrderInfo
				{
					SupplierInfo = new OrderInfoSupplierInfo
                    {
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						FacilityInfo = new FacilityInfo
						{
							Name = SampleData.Depot_LocationID,
						},
						OrderReference = SampleData.Depot_OrderReference,
						OrderStatusInfo = new OrderStatusInfo
						{
							OrderStatus = SampleData.Orderflow_Container_StatusUpdate_Work_Estimate_ByDepot_Status,
							OrderStatusDescription = SampleData.Orderflow_Container_StatusUpdate_Work_Estimate_ByDepot_Status_Description
						},
					},
					ClientInfo = new OrderInfoClientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						},
						OrderReference = SampleData.Lessor_OrderReference
					},
				},
				LocalizationInfo = new LocalizationInfo()
				{
					CurrencyISO = SampleData.Depot_Localization_CurrencyISO,
					ExchangeRate = SampleData.Depot_Localization_CurrencyExchangeRate,
				},
				TankContainerInfo = new TankContainerInfo
				{
					ContainerInfo = new ContainerInfo
					{
						ContainerNumber = SampleData.ContainerNumber,
						ContainerType = SampleData.ContainerType,
						LessorInfo = new ContainerInfoLessorInfo
                        {
							BusinessUnitInfo = new BusinessUnitInfo
                            {
								ExternalID = SampleData.Lessor_ID,
								Name = SampleData.Lessor_ID,
							}
                        }
					},
					TankContainerCharacteristicsInfo = new TankContainerInfoTankContainerCharacteristicsInfo
					{
						ManufacturerInfo = new TankContainerInfoTankContainerCharacteristicsInfoManufacturerInfo
                        {
							BusinessUnitInfo = new BusinessUnitInfo
							{
								ExternalID = SampleData.ContainerManufacturer,
								Name = SampleData.ContainerManufacturer,
							},							
						},
						BuildingYearMonth = DateTime.Parse(SampleData.ContainerBuildingYear),
						Capacity = SampleData.ContainerCapacity,
					},
					InspectionDates = new TankContainerInfoInspectionDates
					{
						LastInspectionDate = DateTime.Parse(SampleData.LastTestDate),
						LastInspectionScopeDescription = SampleData.LastTestType,
						CSCValidityDate = DateTime.Parse(SampleData.CSCDate)
					}
				},
				Container_Work_EstimateStatusInfo = new Container_Work_EstimateStatusInfo
				{
					ContainerWorkStatus = Container_Work_EstimateStatusInfoContainerWorkStatus.Estimate,
					Version = SampleData.Orderflow_Container_Work_Repair_ByDepot_Version,
					SupplierInfo = new Container_Work_EstimateStatusInfoSupplierInfo
                    {
						EstimatorInfo = new Container_Work_EstimateStatusInfoSupplierInfoEstimatorInfo
                        {
							 ContactInfo = new ContactInfo
                             {
								 Name = SampleData.Orderflow_Container_Work_Repair_ByDepot_EstimatorName,
							 }							
						},
						Container_ServiceProgress_DateInfo = new Container_ServiceProgress_DateInfo
						{
							Finished = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Work_ByDepot_Estimate_Finished)
						},
					},
					LessorInfo = new Container_Work_EstimateStatusInfoLessorInfo
                    {
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID,
						},
					},
				},
				ContainerStorageArrivalInfo = new Message_TankContainer_Work_EstimateContainerStorageArrivalInfo
				{
					ContainerDeliveryDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_GateIn_ByDepot)
				},
				LaborRate = SampleData.Depot_Pricing_MandR_LaborRate,
				EstimateLines = new Container_Work_EstimateLinesContainer_Work_EstimateLine[]
				{
					 new Container_Work_EstimateLinesContainer_Work_EstimateLine
					 {
						 LineID = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MandREstimateLineID,
						 OriginID = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MandREstimateLineOriginID,
						 SortOrder = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_SortOrder,
						 Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_Description,
						 CategoryCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineCategoryCode
						 {
							  Code = new Code
							  {
								  Value = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_CategoryCode,
								  Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_CategoryCodeDescription
							  }
						 },
						 GeneralCodes = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodes
						 {
							  DamageCode =  new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesDamageCode
							  {
								  Code = new Code
								  {
									Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_DamageCode,
									Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_DamageCodeDescription
								  }
							  },
							  LocationCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesLocationCode
							  {
								  Code = new Code
								  {
									Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LocationCode,
									Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LocationCodeDescription
								  }

							  },
							  RepairCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesRepairCode
							  {
								  Code = new Code
								  {
									Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_RepairCode,
									Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_RepairCodeDescription
								  }

							  }
						 },
						 Proposal = new Container_Work_EstimateLinesContainer_Work_EstimateLineProposal
						 {
							  Container_Work_EstimateLineProposal = new Container_Work_EstimateLineProposal
							  {
								  LaborRate = SampleData.Depot_Pricing_MandR_LaborRate,
								  LaborHours = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LaborHours,
								  LaborPrice = (SampleData.Depot_Pricing_MandR_LaborRate*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LaborHours),
								  MaterialPrice = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MaterialPrice,
								  LineSubtotal = ((SampleData.Depot_Pricing_MandR_LaborRate*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LaborHours)+SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MaterialPrice),
								  Quantity = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_Quantity,
								  LineTotal = ((SampleData.Depot_Pricing_MandR_LaborRate*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LaborHours)+SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MaterialPrice)*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_Quantity,
								  DoAction = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_DoAction,
							  }
						 }
					 },
					 new Container_Work_EstimateLinesContainer_Work_EstimateLine
					 {
						 LineID = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_MandREstimateLineID,
						 OriginID = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_MandREstimateLineOriginID,
						 SortOrder = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_SortOrder,
						 Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_Description,
						 CategoryCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineCategoryCode
						 {
							  Code = new Code
							  {
								  Value = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_CategoryCode,
								  Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_CategoryCodeDescription
							  }
						 },
						 GeneralCodes = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodes
						 {
							  DamageCode =  new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesDamageCode
							  {
								  Code = new Code
								  {
									Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_DamageCode,
									Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_DamageCodeDescription
								  }
							  },
							  LocationCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesLocationCode
							  {
								  Code = new Code
								  {
									Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LocationCode,
									Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LocationCodeDescription
								  }

							  },
							  RepairCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesRepairCode
							  {
								  Code = new Code
								  {
									Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_RepairCode,
									Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_RepairCodeDescription
								  }

							  }
						 },
						 Proposal = new Container_Work_EstimateLinesContainer_Work_EstimateLineProposal
						 {
							  Container_Work_EstimateLineProposal = new Container_Work_EstimateLineProposal
							  {
								  LaborRate = SampleData.Depot_Pricing_MandR_LaborRate,
								  LaborHours = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LaborHours,
								  LaborPrice = (SampleData.Depot_Pricing_MandR_LaborRate*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LaborHours),
								  MaterialPrice = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_MaterialPrice,
								  LumpSumPrice = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LumpSumPrice,
								  LineSubtotal = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LumpSumPrice,
								  Quantity = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_Quantity,
								  LineTotal = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LumpSumPrice*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_Quantity,
								  DoAction = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_DoAction,
							  }
						 }
					}
				},
				Remarks = new RemarksRemark[]
				{
					new RemarksRemark
					{
						ID = SampleData.Remark_Work_Repair_ID,
						Date = DateTime.Parse(SampleData.Remark_Work_Repair_Date),
						AuthorInfo = new RemarksRemarkAuthorInfo
						{
							 ContactInfo = new ContactInfo
							 {
								 Name = SampleData.Remark_Work_Repair_Author,
							 }
						},
						Remark = SampleData.Remark_Work_Repair
					}
				}

			};
			return m;
		}


        static Message_Container_Work_Estimate sample_Message_Container_Work_Estimate_Authorization()
        {
            Message_Container_Work_Estimate m = new Message_Container_Work_Estimate
            {
                MessageHeaderInfo = new MessageHeaderInfo
                {
                    MessageID = SampleData.MessageIdentifier_Container_Work_Repair_Estimate_MessageID,
                    ConversationID = SampleData.MessageIdentifier_Container_Work_Repair_ConversationID,
                    SentDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Work_Repair_ByDepot_LessorAuthorizationDate),
                    OTFVersion = OTFVersion,
                    OTFMessage = MessageHeaderInfoOTFMessage.Message_Container_Work_Estimate,
                    MessageType = MessageHeaderInfoMessageType.New,

                    SenderInfo = new MessageHeaderInfoSenderInfo
                    {
                        BusinessUnitInfo = new BusinessUnitInfo
                        {
                            ExternalID = SampleData.Lessor_ID,
                            Name = SampleData.Lessor_ID
                        }
                    },
                    RecipientInfo = new MessageHeaderInfoRecipientInfo
                    {
                        BusinessUnitInfo = new BusinessUnitInfo
                        {
                            ExternalID = SampleData.Depot_ID,
                            Name = SampleData.Depot_ID
                        }
                    },
                },
                OrderInfo = new OrderInfo
                {
                    SupplierInfo = new OrderInfoSupplierInfo
                    {
                        BusinessUnitInfo = new BusinessUnitInfo
                        {
                            ExternalID = SampleData.Depot_ID,
                            Name = SampleData.Depot_ID
                        },
                        FacilityInfo = new FacilityInfo
                        {
                            Name = SampleData.Depot_LocationID,
                        },
                        OrderReference = SampleData.Depot_OrderReference,
                        OrderStatusInfo = new OrderStatusInfo
                        {
                            OrderStatus = SampleData.Orderflow_Container_StatusUpdate_Work_Estimate_ByDepot_Status,
                            OrderStatusDescription = SampleData.Orderflow_Container_StatusUpdate_Work_Estimate_ByDepot_Status_Description
                        },
                    },
                    ClientInfo = new OrderInfoClientInfo
                    {
                        BusinessUnitInfo = new BusinessUnitInfo
                        {
                            ExternalID = SampleData.Lessor_ID,
                            Name = SampleData.Lessor_ID
                        },
                        OrderReference = SampleData.Lessor_OrderReference
                    },
                },
                LocalizationInfo = new LocalizationInfo()
                {
                    CurrencyISO = SampleData.Depot_Localization_CurrencyISO,
                    ExchangeRate = SampleData.Depot_Localization_CurrencyExchangeRate,
                },

                ContainerInfo = new ContainerInfo
                {
                    ContainerNumber = SampleData.ContainerNumber,
                    ContainerType = SampleData.ContainerType
                },

                Container_Work_EstimateStatusInfo = new Container_Work_EstimateStatusInfo
                {
					ContainerWorkStatus = Container_Work_EstimateStatusInfoContainerWorkStatus.Authorization,
                    Version = SampleData.Orderflow_Container_Work_Repair_ByDepot_Version,
                    SupplierInfo = new Container_Work_EstimateStatusInfoSupplierInfo
                    {
                        EstimatorInfo = new Container_Work_EstimateStatusInfoSupplierInfoEstimatorInfo
                        {
                            ContactInfo = new ContactInfo
                            {
                                Name = SampleData.Orderflow_Container_Work_Repair_ByDepot_EstimatorName,
                            }
                        },
                        Container_ServiceProgress_DateInfo = new Container_ServiceProgress_DateInfo
                        {
                            Finished = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Work_ByDepot_Estimate_Finished)
                        },
                    },
                    LessorInfo = new Container_Work_EstimateStatusInfoLessorInfo
                    {
                        BusinessUnitInfo = new BusinessUnitInfo
                        {
                            ExternalID = SampleData.Lessor_ID,
                            Name = SampleData.Lessor_ID,
                        },
                        Container_ServiceProgress_AuthorizationInfo = new Container_ServiceProgress_AuthorizationInfo
                        {
                            AuthorizationReference = SampleData.Lessor_Work_RepairServiceRequest_AuthorizationReference,
                            Authorized = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Work_Repair_ByDepot_LessorAuthorizationDate)
                        }
                    },
                },
                ContainerStorageArrivalInfo = new Message_Container_Work_EstimateContainerStorageArrivalInfo
                {
                    ContainerDeliveryDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_GateIn_ByDepot)
                },
                LaborRate = SampleData.Depot_Pricing_MandR_LaborRate,
                EstimateLines = new Container_Work_EstimateLinesContainer_Work_EstimateLine[]
                {
                     new Container_Work_EstimateLinesContainer_Work_EstimateLine
                     {
                         LineID = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MandREstimateLineID,
                         OriginID = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MandREstimateLineOriginID,
                         SortOrder = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_SortOrder,
                         Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_Description,
                         CategoryCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineCategoryCode
                         {
                              Code = new Code
                              {
                                  Value = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_CategoryCode,
                                  Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_CategoryCodeDescription
                              }
                         },
                         GeneralCodes = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodes
                         {
                              DamageCode =  new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesDamageCode
                              {
                                  Code = new Code
                                  {
                                    Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_DamageCode,
                                    Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_DamageCodeDescription
                                  }
                              },
                              LocationCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesLocationCode
                              {
                                  Code = new Code
                                  {
                                    Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LocationCode,
                                    Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LocationCodeDescription
                                  }

                              },
                              RepairCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesRepairCode
                              {
                                  Code = new Code
                                  {
                                    Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_RepairCode,
                                    Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_RepairCodeDescription
                                  }

                              }
                         },
                         Proposal = new Container_Work_EstimateLinesContainer_Work_EstimateLineProposal
                         {

                              Container_Work_EstimateLineProposal = new Container_Work_EstimateLineProposal
                              {
                                  LaborRate = SampleData.Depot_Pricing_MandR_LaborRate,
                                  LaborHours = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LaborHours,
                                  LaborPrice = (SampleData.Depot_Pricing_MandR_LaborRate*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LaborHours),
                                  MaterialPrice = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MaterialPrice,
                                  LineSubtotal = ((SampleData.Depot_Pricing_MandR_LaborRate*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LaborHours)+SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MaterialPrice),
                                  Quantity = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_Quantity,
                                  LineTotal = ((SampleData.Depot_Pricing_MandR_LaborRate*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LaborHours)+SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MaterialPrice)*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_Quantity,
                                  DoAction = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_DoAction,
                              }
                         }
                     },
                     new Container_Work_EstimateLinesContainer_Work_EstimateLine
                     {
                         LineID = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_MandREstimateLineID,
                         OriginID = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_MandREstimateLineOriginID,
                         SortOrder = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_SortOrder,
                         Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_Description,
                         CategoryCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineCategoryCode
                         {
                              Code = new Code
                              {
                                  Value = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_CategoryCode,
                                  Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_CategoryCodeDescription
                              }
                         },
                         GeneralCodes = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodes
                         {
                              DamageCode =  new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesDamageCode
                              {
                                  Code = new Code
                                  {
                                    Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_DamageCode,
                                    Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_DamageCodeDescription
                                  }
                              },
                              LocationCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesLocationCode
                              {
                                  Code = new Code
                                  {
                                    Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LocationCode,
                                    Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LocationCodeDescription
                                  }

                              },
                              RepairCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesRepairCode
                              {
                                  Code = new Code
                                  {
                                    Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_RepairCode,
                                    Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_RepairCodeDescription
                                  }

                              }
                         },
                         Proposal = new Container_Work_EstimateLinesContainer_Work_EstimateLineProposal
                         {
                              Container_Work_EstimateLineProposal = new Container_Work_EstimateLineProposal
                              {
                                  LaborRate = SampleData.Depot_Pricing_MandR_LaborRate,
                                  LaborHours = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LaborHours,
                                  LaborPrice = (SampleData.Depot_Pricing_MandR_LaborRate*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LaborHours),
                                  MaterialPrice = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_MaterialPrice,
                                  LumpSumPrice = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LumpSumPrice,
                                  LineSubtotal = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LumpSumPrice,
                                  Quantity = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_Quantity,
                                  LineTotal = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LumpSumPrice*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_Quantity,
                                  DoAction = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_DoAction,
                              }
                         }
                    }
                },
                Remarks = new RemarksRemark[]
                {
                    new RemarksRemark
                    {
                        ID = SampleData.Remark_Work_Repair_ID,
                        Date = DateTime.Parse(SampleData.Remark_Work_Repair_Date),
                        AuthorInfo = new RemarksRemarkAuthorInfo
                        {
                             ContactInfo = new ContactInfo
                             {
                                 Name = SampleData.Remark_Work_Repair_Author,
                             }
                        },
                        Remark = SampleData.Remark_Work_Repair
                    }
                }

            };
            return m;
        }
        static Message_TankContainer_Work_Estimate sample_Message_TankContainer_Work_Estimate_Authorization()
        {
            Message_TankContainer_Work_Estimate m = new Message_TankContainer_Work_Estimate
            {
                MessageHeaderInfo = new MessageHeaderInfo
                {
                    MessageID = SampleData.MessageIdentifier_Container_Work_Repair_Estimate_MessageID,
                    ConversationID = SampleData.MessageIdentifier_Container_Work_Repair_ConversationID,
                    SentDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Work_Repair_ByDepot_LessorAuthorizationDate),
                    OTFVersion = OTFVersion,
                    OTFMessage = MessageHeaderInfoOTFMessage.Message_TankContainer_Work_Estimate,
                    MessageType = MessageHeaderInfoMessageType.New,

                    SenderInfo = new MessageHeaderInfoSenderInfo
                    {
                        BusinessUnitInfo = new BusinessUnitInfo
                        {
                            ExternalID = SampleData.Lessor_ID,
                            Name = SampleData.Lessor_ID
                        }
                    },
                    RecipientInfo = new MessageHeaderInfoRecipientInfo
                    {
                        BusinessUnitInfo = new BusinessUnitInfo
                        {
                            ExternalID = SampleData.Depot_ID,
                            Name = SampleData.Depot_ID
                        }
                    },
                },
                OrderInfo = new OrderInfo
                {
                    SupplierInfo = new OrderInfoSupplierInfo
                    {
                        BusinessUnitInfo = new BusinessUnitInfo
                        {
                            ExternalID = SampleData.Depot_ID,
                            Name = SampleData.Depot_ID
                        },
                        FacilityInfo = new FacilityInfo
                        {
                            Name = SampleData.Depot_LocationID,
                        },
                        OrderReference = SampleData.Depot_OrderReference,
                        OrderStatusInfo = new OrderStatusInfo
                        {
                            OrderStatus = SampleData.Orderflow_Container_StatusUpdate_Work_Estimate_ByDepot_Status,
                            OrderStatusDescription = SampleData.Orderflow_Container_StatusUpdate_Work_Estimate_ByDepot_Status_Description
                        },
                    },
                    ClientInfo = new OrderInfoClientInfo
                    {
                        BusinessUnitInfo = new BusinessUnitInfo
                        {
                            ExternalID = SampleData.Lessor_ID,
                            Name = SampleData.Lessor_ID
                        },
                        OrderReference = SampleData.Lessor_OrderReference
                    },
                },
                LocalizationInfo = new LocalizationInfo()
                {
                    CurrencyISO = SampleData.Depot_Localization_CurrencyISO,
                    ExchangeRate = SampleData.Depot_Localization_CurrencyExchangeRate,
                },
                TankContainerInfo = new TankContainerInfo
                {
                    ContainerInfo = new ContainerInfo
                    {
                        ContainerNumber = SampleData.ContainerNumber,
                        ContainerType = SampleData.ContainerType,
                        LessorInfo = new ContainerInfoLessorInfo
                        {
                            BusinessUnitInfo = new BusinessUnitInfo
                            {
                                ExternalID = SampleData.Lessor_ID,
                                Name = SampleData.Lessor_ID,
                            }
                        }
                    },
                    TankContainerCharacteristicsInfo = new TankContainerInfoTankContainerCharacteristicsInfo
                    {
                        ManufacturerInfo = new TankContainerInfoTankContainerCharacteristicsInfoManufacturerInfo
                        {
                            BusinessUnitInfo = new BusinessUnitInfo
                            {
                                ExternalID = SampleData.ContainerManufacturer,
                                Name = SampleData.ContainerManufacturer,
                            },
                        },
                        BuildingYearMonth = DateTime.Parse(SampleData.ContainerBuildingYear),
                        Capacity = SampleData.ContainerCapacity,
                    },
                    InspectionDates = new TankContainerInfoInspectionDates
                    {
                        LastInspectionDate = DateTime.Parse(SampleData.LastTestDate),
                        LastInspectionScopeDescription = SampleData.LastTestType,
                        CSCValidityDate = DateTime.Parse(SampleData.CSCDate)
                    }
                },
                Container_Work_EstimateStatusInfo = new Container_Work_EstimateStatusInfo
                {
					ContainerWorkStatus = Container_Work_EstimateStatusInfoContainerWorkStatus.Authorization,
                    Version = SampleData.Orderflow_Container_Work_Repair_ByDepot_Version,
                    SupplierInfo = new Container_Work_EstimateStatusInfoSupplierInfo
                    {
                        EstimatorInfo = new Container_Work_EstimateStatusInfoSupplierInfoEstimatorInfo
                        {
                            ContactInfo = new ContactInfo
                            {
                                Name = SampleData.Orderflow_Container_Work_Repair_ByDepot_EstimatorName,
                            }
                        },
                        Container_ServiceProgress_DateInfo = new Container_ServiceProgress_DateInfo
                        {
                            Finished = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Work_ByDepot_Estimate_Finished)
                        },
                    },
                    LessorInfo = new Container_Work_EstimateStatusInfoLessorInfo
                    {
                        BusinessUnitInfo = new BusinessUnitInfo
                        {
                            ExternalID = SampleData.Lessor_ID,
                            Name = SampleData.Lessor_ID,
                        },
                        Container_ServiceProgress_AuthorizationInfo = new Container_ServiceProgress_AuthorizationInfo
                        {
                            AuthorizationReference = SampleData.Lessor_Work_RepairServiceRequest_AuthorizationReference,
                            Authorized = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Work_Repair_ByDepot_LessorAuthorizationDate)
                        }
                    },
                },
                ContainerStorageArrivalInfo = new Message_TankContainer_Work_EstimateContainerStorageArrivalInfo
                {
                    ContainerDeliveryDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_GateIn_ByDepot)
                },
                LaborRate = SampleData.Depot_Pricing_MandR_LaborRate,
                EstimateLines = new Container_Work_EstimateLinesContainer_Work_EstimateLine[]
                {
                     new Container_Work_EstimateLinesContainer_Work_EstimateLine
                     {
                         LineID = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MandREstimateLineID,
                         OriginID = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MandREstimateLineOriginID,
                         SortOrder = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_SortOrder,
                         Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_Description,
                         CategoryCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineCategoryCode
                         {
                              Code = new Code
                              {
                                  Value = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_CategoryCode,
                                  Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_CategoryCodeDescription
                              }
                         },
                         GeneralCodes = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodes
                         {
                              DamageCode =  new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesDamageCode
                              {
                                  Code = new Code
                                  {
                                    Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_DamageCode,
                                    Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_DamageCodeDescription
                                  }
                              },
                              LocationCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesLocationCode
                              {
                                  Code = new Code
                                  {
                                    Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LocationCode,
                                    Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LocationCodeDescription
                                  }

                              },
                              RepairCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesRepairCode
                              {
                                  Code = new Code
                                  {
                                    Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_RepairCode,
                                    Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_RepairCodeDescription
                                  }

                              }
                         },
                         Proposal = new Container_Work_EstimateLinesContainer_Work_EstimateLineProposal
                         {
                              Container_Work_EstimateLineProposal = new Container_Work_EstimateLineProposal
                              {
                                  LaborRate = SampleData.Depot_Pricing_MandR_LaborRate,
                                  LaborHours = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LaborHours,
                                  LaborPrice = (SampleData.Depot_Pricing_MandR_LaborRate*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LaborHours),
                                  MaterialPrice = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MaterialPrice,
                                  LineSubtotal = ((SampleData.Depot_Pricing_MandR_LaborRate*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LaborHours)+SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MaterialPrice),
                                  Quantity = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_Quantity,
                                  LineTotal = ((SampleData.Depot_Pricing_MandR_LaborRate*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_LaborHours)+SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_MaterialPrice)*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_Quantity,
                                  DoAction = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_DoAction,
                              }
                         }
                     },
                     new Container_Work_EstimateLinesContainer_Work_EstimateLine
                     {
                         LineID = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_MandREstimateLineID,
                         OriginID = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_MandREstimateLineOriginID,
                         SortOrder = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_SortOrder,
                         Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_Description,
                         CategoryCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineCategoryCode
                         {
                              Code = new Code
                              {
                                  Value = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_CategoryCode,
                                  Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_CategoryCodeDescription
                              }
                         },
                         GeneralCodes = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodes
                         {
                              DamageCode =  new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesDamageCode
                              {
                                  Code = new Code
                                  {
                                    Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_DamageCode,
                                    Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_DamageCodeDescription
                                  }
                              },
                              LocationCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesLocationCode
                              {
                                  Code = new Code
                                  {
                                    Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LocationCode,
                                    Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LocationCodeDescription
                                  }

                              },
                              RepairCode = new Container_Work_EstimateLinesContainer_Work_EstimateLineGeneralCodesRepairCode
                              {
                                  Code = new Code
                                  {
                                    Value =  SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_RepairCode,
                                    Description = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_RepairCodeDescription
                                  }

                              }
                         },
                         Proposal = new Container_Work_EstimateLinesContainer_Work_EstimateLineProposal
                         {
                              Container_Work_EstimateLineProposal = new Container_Work_EstimateLineProposal
                              {
                                  LaborRate = SampleData.Depot_Pricing_MandR_LaborRate,
                                  LaborHours = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LaborHours,
                                  LaborPrice = (SampleData.Depot_Pricing_MandR_LaborRate*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LaborHours),
                                  MaterialPrice = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_MaterialPrice,
                                  LumpSumPrice = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LumpSumPrice,
                                  LineSubtotal = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LumpSumPrice,
                                  Quantity = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_Quantity,
                                  LineTotal = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_LumpSumPrice*SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo2_Quantity,
                                  DoAction = SampleData.Orderflow_Container_Work_Repair_EstimateLineInfo1_DoAction,
                              }
                         }
                    }
                },
                Remarks = new RemarksRemark[]
                {
                    new RemarksRemark
                    {
                        ID = SampleData.Remark_Work_Repair_ID,
                        Date = DateTime.Parse(SampleData.Remark_Work_Repair_Date),
                        AuthorInfo = new RemarksRemarkAuthorInfo
                        {
                             ContactInfo = new ContactInfo
                             {
                                 Name = SampleData.Remark_Work_Repair_Author,
                             }
                        },
                        Remark = SampleData.Remark_Work_Repair
                    }
                }

            };
            return m;
        }


        static Message_Container_StatusUpdate_Work sample_Message_Container_StatusUpdate_Work()
		{
			Message_Container_StatusUpdate_Work m = new Message_Container_StatusUpdate_Work
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_Container_Work_Repair_StatusUpdate_MessageID,
					ConversationID = SampleData.MessageIdentifier_Container_Work_Repair_ConversationID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Work_Repair_ByDepot_Work_Started),
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_Container_StatusUpdate_Work,
					MessageType = MessageHeaderInfoMessageType.New,

					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						}
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
				},
				OrderInfo = new OrderInfo
				{
					SupplierInfo = new OrderInfoSupplierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						FacilityInfo = new FacilityInfo
						{
							Name = SampleData.Depot_LocationID,
						},
						OrderReference = SampleData.Depot_OrderReference,
						OrderStatusInfo = new OrderStatusInfo
						{
							OrderStatus = SampleData.Orderflow_Container_StatusUpdate_Work_Repair_ByDepot_Status,
							OrderStatusDescription = SampleData.Orderflow_Container_StatusUpdate_Work_Repair_ByDepot_Status_Description
						},
					},
					ClientInfo = new OrderInfoClientInfo
                    {
						BusinessUnitInfo = new BusinessUnitInfo
                        {
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID,
						},
					OrderReference = SampleData.Lessor_OrderReference
					},
				},
				ContainerInfo = new ContainerInfo
				{
					ContainerNumber = SampleData.ContainerNumber,
					ContainerType = SampleData.ContainerType
				},
				SupplierReference = SampleData.Depot_OrderReference_Work,
				Container_Work_EstimateStatusInfo = new Container_Work_EstimateStatusInfo
				{
					ContainerWorkStatus = Container_Work_EstimateStatusInfoContainerWorkStatus.Accept,
					Version = SampleData.Orderflow_Container_Work_Repair_ByDepot_Version,
					SupplierInfo = new Container_Work_EstimateStatusInfoSupplierInfo
                    {
						EstimatorInfo = new Container_Work_EstimateStatusInfoSupplierInfoEstimatorInfo
                        {
							ContactInfo = new ContactInfo
                            {
								Name = SampleData.Orderflow_Container_Work_Repair_ByDepot_EstimatorName,
							}
                        },
						Container_ServiceProgress_DateInfo = new Container_ServiceProgress_DateInfo
						{
							Finished = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Work_ByDepot_Estimate_Finished)
						},
					},
					LessorInfo = new Container_Work_EstimateStatusInfoLessorInfo
                    {
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID,
						},
						Container_ServiceProgress_AuthorizationInfo = new Container_ServiceProgress_AuthorizationInfo
						{
							AuthorizationReference = SampleData.Lessor_Work_RepairServiceRequest_AuthorizationReference,
							Authorized = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Work_Repair_ByDepot_LessorAuthorizationDate)
						}
					}
				},
				Container_Work_WorkStatusInfo = new Message_Container_StatusUpdate_WorkContainer_Work_WorkStatusInfo
				{
					Container_ServiceProgress_DateInfo = new Container_ServiceProgress_DateInfo
					{
						Started = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Work_Repair_ByDepot_Work_Started)
					}
				}
			};
			return m;
		}
		static Message_TankContainer_StatusUpdate_Work sample_Message_TankContainer_StatusUpdate_Work()
		{
			Message_TankContainer_StatusUpdate_Work m = new Message_TankContainer_StatusUpdate_Work
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_Container_Work_Repair_StatusUpdate_MessageID,
					ConversationID = SampleData.MessageIdentifier_Container_Work_Repair_ConversationID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Work_Repair_ByDepot_Work_Started),
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_TankContainer_StatusUpdate_Work,
					MessageType = MessageHeaderInfoMessageType.New,

					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						}
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
				},
				OrderInfo = new OrderInfo
				{
					SupplierInfo = new OrderInfoSupplierInfo
                    {
						BusinessUnitInfo = new BusinessUnitInfo
                        {
							ExternalID = SampleData.Depot_ID,
							Name = SampleData.Depot_ID
						},
						FacilityInfo = new FacilityInfo
						{
							Name = SampleData.Depot_LocationID,
						},
						OrderReference = SampleData.Depot_OrderReference,
						OrderStatusInfo = new OrderStatusInfo
						{
							OrderStatus = SampleData.Orderflow_Container_StatusUpdate_Work_Repair_ByDepot_Status,
							OrderStatusDescription = SampleData.Orderflow_Container_StatusUpdate_Work_Repair_ByDepot_Status_Description
						},
					},
					ClientInfo = new OrderInfoClientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						},
						OrderReference = SampleData.Lessor_OrderReference
					},
				},
				TankContainerInfo = new TankContainerInfo
				{
					ContainerInfo = new ContainerInfo
					{
						ContainerNumber = SampleData.ContainerNumber,
						ContainerType = SampleData.ContainerType
					}
				},
				SupplierReference = SampleData.Depot_OrderReference_Work,
				Container_Work_EstimateStatusInfo = new Container_Work_EstimateStatusInfo
				{
                    ContainerWorkStatus = Container_Work_EstimateStatusInfoContainerWorkStatus.Accept,
                    Version = SampleData.Orderflow_Container_Work_Repair_ByDepot_Version,
					SupplierInfo = new Container_Work_EstimateStatusInfoSupplierInfo
                    {
						EstimatorInfo = new Container_Work_EstimateStatusInfoSupplierInfoEstimatorInfo
                        {
							ContactInfo = new ContactInfo
							{
								Name = SampleData.Orderflow_Container_Work_Repair_ByDepot_EstimatorName,
							}
						},
						Container_ServiceProgress_DateInfo = new Container_ServiceProgress_DateInfo
						{
							Finished = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Work_ByDepot_Estimate_Finished)
						},
					},
					LessorInfo = new Container_Work_EstimateStatusInfoLessorInfo
                    {
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						},
						Container_ServiceProgress_AuthorizationInfo = new Container_ServiceProgress_AuthorizationInfo
						{
							AuthorizationReference = SampleData.Lessor_Work_RepairServiceRequest_AuthorizationReference,
							Authorized = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Work_Repair_ByDepot_LessorAuthorizationDate)
						}
					}
				},
				Container_Work_WorkStatusInfo = new Message_TankContainer_StatusUpdate_WorkContainer_Work_WorkStatusInfo
				{
					Container_ServiceProgress_DateInfo = new Container_ServiceProgress_DateInfo
					{
						Started = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Work_Repair_ByDepot_Work_Started)
					}
				}
			};
			return m;
		}
		#endregion

		#region "Inspection Certificates"
		static Message_TankContainer_InspectionReport sample_Message_TankContainer_InspectionReport()
		{
			Message_TankContainer_InspectionReport m = new Message_TankContainer_InspectionReport
			{
				MessageHeaderInfo = new MessageHeaderInfo
				{
					MessageID = SampleData.MessageIdentifier_TankContainer_Inspection_MessageID,
					SentDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Inspection_ByDepot_Started),
					OTFVersion = OTFVersion,
					OTFMessage = MessageHeaderInfoOTFMessage.Message_TankContainer_InspectionReport,
					MessageType = MessageHeaderInfoMessageType.New,

					SenderInfo = new MessageHeaderInfoSenderInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.InspectionBureauID,
							Name = SampleData.InspectionBureauID
						}
					},
					RecipientInfo = new MessageHeaderInfoRecipientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
				},
				InspectionOrderInfo = new Message_TankContainer_InspectionReportInspectionOrderInfo
				{
					SupplierInfo = new Message_TankContainer_InspectionReportInspectionOrderInfoSupplierInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.InspectionBureauID,
							Name = SampleData.InspectionBureauID
						}
					},
					SupplierInspectionReference = "",
					SupplierInspectionRevision = "",
					SupplierInspectionRevisionRemarks = new RemarksRemark[]
					{
					},
					SupplierTermsAndConditions = "",
					InspectionScope = TankContainerInfoInspectionDatesLastInspectionScope.Item5YearTest,
					ClientInfo = new Message_TankContainer_InspectionReportInspectionOrderInfoClientInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID,
						}
					},
					ClientInspectionReference = SampleData.Lessor_OrderReference,
				},
				InspectionResultsInfo = new Message_TankContainer_InspectionReportInspectionResultsInfo
				{
					InspectionLocationInfo = new Message_TankContainer_InspectionReportInspectionResultsInfoInspectionLocationInfo
                    {
						FacilityInfo = new FacilityInfo
                        {
							Name = SampleData.Depot_LocationID,
						}
                    },
					InspectionCenterID = "",
					InspectionDate = DateTime.Parse(SampleData.Orderflow_Container_StatusUpdate_Inspection_ByDepot_Started),

					OperatorInfo = new Message_TankContainer_InspectionReportInspectionResultsInfoOperatorInfo
					{
						BusinessUnitInfo = new BusinessUnitInfo
						{
							ExternalID = SampleData.Lessor_ID,
							Name = SampleData.Lessor_ID
						}
					},
					TankContainerMarkings = new RemarksRemark[]
					{

					},
					TankContainerRegulationsApplicableAccordingToMarkingsInfo = new Message_TankContainer_InspectionReportInspectionResultsInfoTankContainerRegulationsApplicableAccordingToMarkingsInfo
					{
						IMDG = SampleData.Inspection_IMDG,
						RID_ADR = SampleData.Inspection_RID_ADR,
						CSC = SampleData.Inspection_CSC,
						USDOT = SampleData.Inspection_USDOT
					},

					TankContainerInfo = new TankContainerInfo
					{
						ContainerInfo = new ContainerInfo
						{
							ContainerNumber = SampleData.ContainerNumber,
							ContainerType = SampleData.ContainerType,
							LessorInfo = new ContainerInfoLessorInfo
							{
								BusinessUnitInfo = new BusinessUnitInfo
								{
									ExternalID = SampleData.Lessor_ID,
									Name = SampleData.Lessor_ID,
								}
							}
						},

						TankContainerSuitabilityInfo = new TankContainerInfoTankContainerSuitabilityInfo
						{
							PortableTankType = TankContainerInfoTankContainerSuitabilityInfoPortableTankType.T11,
							RID_ADRCode = SampleData.Inspection_RID_ADRCode,
							SubstancesSuitableForTransport = new TankContainerInfo_SubstancesSuitableForTransportTankContainerInfo_SubstanceSuitableForTransport[]
							{
								new TankContainerInfo_SubstancesSuitableForTransportTankContainerInfo_SubstanceSuitableForTransport
								{
									UNNumber = ""
								}
							},
							SpecialProvisions = new TankContainerInfo_SpecialProvisionsTankContainerInfo_SpecialProvision[]
							{
								new TankContainerInfo_SpecialProvisionsTankContainerInfo_SpecialProvision
								{
									SpecialProvision = ""
								}
							}


						},
						TankContainerCharacteristicsInfo = new TankContainerInfoTankContainerCharacteristicsInfo
						{
							ManufacturerInfo = new TankContainerInfoTankContainerCharacteristicsInfoManufacturerInfo
							{
								BusinessUnitInfo = new BusinessUnitInfo
								{
									ExternalID = SampleData.ContainerManufacturer,
									Name = SampleData.ContainerManufacturer,
								}
							},
							BuildingYearMonth = DateTime.Parse(SampleData.ContainerBuildingYear),
							ISO6346SizeTypeCode = TankContainerInfoTankContainerCharacteristicsInfoISO6346SizeTypeCode.Item20G0,
							ISO6346SizeTypeCodeDescription = "",
							DimensionsXMillimeter = 0,
							DimensionsYMillimeter = 0,
							DimensionsZMillimeter = 0,
							MaxGrossMass = 0,
							Payload = 0,
							Tare = 0,
							Capacity = SampleData.ContainerCapacity
						},

						TankContainerTankInfo = new TankContainerInfoTankContainerTankInfo
						{
							CompartmentQuantity = 1,
							ShellMaterial = "",
							ShellEquivalentThicknessRefSteelMillimeter = 0,
							SurgePlates = false,
							SurgePlatesCapacityBetweenIsLowerThan7500L = false,
							WorkPressureMaximumBar = 0,
							DesignPressureExternalBar = 0,
							TestPressureHydraulicBar = 0,
							DesignTemperatureMinimum = 0,
							DesignTemperatureMaximum = 0,
							DesignTemperatureUnit = TankContainerInfoTankContainerTankInfoDesignTemperatureUnit.Celcius

						},
						TankContainerEquipmentInfo = new TankContainerInfoTankContainerEquipmentInfo
						{
							TopDischarge = "",
							TopQuantityOfEnclosuresInSeries = 0,
							BottomDischarge = "",
							BottomQuantityOfEnclosuresInSeries = 0,
							Heater = "",
							HeaterWorkingPressureBar = 0,
							ReliefValveQuantity = 0,
							ReliefValves = new TankContainerInfo_ReliefValvesTankContainerInfo_ReliefValve[]
							{
								new TankContainerInfo_ReliefValvesTankContainerInfo_ReliefValve
								{
									Manufacturer = SampleData.SparePartsManufacturer,
									SerialNumber = "",
									SettingPlus = 0
								}
							},
							RuptureDiscQuantity = 0,
						},
						TankContainerLiningInfo = new TankContainerInfoTankContainerLiningInfo
						{
							InternalCoating = "",
							Insulation = "",
							Material = ""
						},
						InspectionDates = new TankContainerInfoInspectionDates
						{
							LastInspectionDate = DateTime.Parse(SampleData.LastTestDate),
							LastInspectionScopeDescription = SampleData.LastTestType,
							CSCValidityDate = DateTime.Parse(SampleData.CSCDate)
						}
					},

					Inspections = new Message_TankContainer_InspectionReportInspectionResultsInfoInspections
					{
						InternalInspection = new Message_TankContainer_InspectionReportInspectionResultsInfoInspectionsInternalInspection
						{
							TankContainer_Inspection = new TankContainer_Inspection
							{
								InspectionResult = TankContainer_InspectionInspectionResult.SatisfactoryWithRemark,
								InspectionRemark = SampleData.InspectionRemark,
							}
						},
						ExternalInspection = new Message_TankContainer_InspectionReportInspectionResultsInfoInspectionsExternalInspection
						{
							TankContainer_Inspection = new TankContainer_Inspection
							{
								InspectionResult = TankContainer_InspectionInspectionResult.Satisfactory
							}
						},
						ThicknessMeasurements = new Message_TankContainer_InspectionReportInspectionResultsInfoInspectionsThicknessMeasurements
						{
							TankContainer_Inspection = new TankContainer_Inspection
							{
								InspectionResult = TankContainer_InspectionInspectionResult.Remark,
								InspectionRemark = SampleData.InspectionRemark,
							}
						},
						HydraulicTest = new Message_TankContainer_InspectionReportInspectionResultsInfoInspectionsHydraulicTest
						{
							TankContainer_Inspection = new TankContainer_Inspection
							{
								InspectionResult = TankContainer_InspectionInspectionResult.NotApplicable
							}
						},
						LeakproofnessTest = new Message_TankContainer_InspectionReportInspectionResultsInfoInspectionsLeakproofnessTest
						{
							TankContainer_Inspection = new TankContainer_Inspection
							{
								InspectionResult = TankContainer_InspectionInspectionResult.NotApplicable
							}
						},
						VacuumMeasurement = new Message_TankContainer_InspectionReportInspectionResultsInfoInspectionsVacuumMeasurement
						{
							TankContainer_Inspection = new TankContainer_Inspection
							{
								InspectionResult = TankContainer_InspectionInspectionResult.NotApplicable
							}
						},
						HeaterPressureTest = new Message_TankContainer_InspectionReportInspectionResultsInfoInspectionsHeaterPressureTest
						{
							TankContainer_Inspection = new TankContainer_Inspection
							{
								InspectionResult = TankContainer_InspectionInspectionResult.NotApplicable
							}
						},
						ValveSettingsCheck = new Message_TankContainer_InspectionReportInspectionResultsInfoInspectionsValveSettingsCheck
						{
							TankContainer_Inspection = new TankContainer_Inspection
							{
								InspectionResult = TankContainer_InspectionInspectionResult.NotApplicable
							}
						},
						ServiceEquipmentCheck = new Message_TankContainer_InspectionReportInspectionResultsInfoInspectionsServiceEquipmentCheck
						{
							TankContainer_Inspection = new TankContainer_Inspection
							{
								InspectionResult = TankContainer_InspectionInspectionResult.NotApplicable
							}
						},
						FrameExamination = new Message_TankContainer_InspectionReportInspectionResultsInfoInspectionsFrameExamination
						{
							TankContainer_Inspection = new TankContainer_Inspection
							{
								InspectionResult = TankContainer_InspectionInspectionResult.NotApplicable
							}
						},
						MarkingExamination = new Message_TankContainer_InspectionReportInspectionResultsInfoInspectionsMarkingExamination
						{
							TankContainer_Inspection = new TankContainer_Inspection
							{
								InspectionResult = TankContainer_InspectionInspectionResult.NotApplicable
							}
						},
						RefOfMeasurementInstrumentsUsed = ""
					}
				},
				InspectionVerification = new Message_TankContainer_InspectionReportInspectionVerification
				{
					VerificationDate = DateTime.Now,
					VerificatorInfo = new Message_TankContainer_InspectionReportInspectionVerificationVerificatorInfo
					{
						ContactInfo = new ContactInfo
						{
							Name = SampleData.SurveyorName
						}
					}
				},
				Attachments = new AttachmentsAttachment[]
				{
					new AttachmentsAttachment
					{
						 URL = SampleData.InspectionHardcopyURL
                    }
                }
			};

			return m;
		} 
		#endregion
	}
}