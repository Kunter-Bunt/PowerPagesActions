#pragma warning disable CS1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PowerPagesCustomApiAdapter.EarlyBounds
{
	
	
	/// <summary>
	/// Status of the Power Pages Component
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	public enum PowerPageComponent_StateCode
	{
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Active", 0)]
		Active = 0,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Inactive", 1)]
		Inactive = 1,
	}
	
	/// <summary>
	/// Reason for the status of the Power Pages Component
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	public enum PowerPageComponent_StatusCode
	{
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Active", 0)]
		Active = 1,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Inactive", 1)]
		Inactive = 2,
	}
	
	[System.Runtime.Serialization.DataContractAttribute()]
	[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("powerpagecomponent")]
	public partial class PowerPageComponent : Microsoft.Xrm.Sdk.Entity
	{
		
		/// <summary>
		/// Available fields, a the time of codegen, for the powerpagecomponent entity
		/// </summary>
		public partial class Fields
		{
			public const string ComponentIdUnique = "componentidunique";
			public const string ComponentState = "componentstate";
			public const string ComponentStateName = "componentstatename";
			public const string Content = "content";
			public const string CreatedBy = "createdby";
			public const string CreatedByName = "createdbyname";
			public const string CreatedByYomiName = "createdbyyominame";
			public const string CreatedOn = "createdon";
			public const string CreatedOnBehalfBy = "createdonbehalfby";
			public const string CreatedOnBehalfByName = "createdonbehalfbyname";
			public const string CreatedOnBehalfByYomiName = "createdonbehalfbyyominame";
			public const string FileContent = "filecontent";
			public const string FileContent_Name = "filecontent_name";
			public const string ImportSequenceNumber = "importsequencenumber";
			public const string IsCustomizable = "iscustomizable";
			public const string IsManaged = "ismanaged";
			public const string IsManagedName = "ismanagedname";
			public const string ModifiedBy = "modifiedby";
			public const string ModifiedByName = "modifiedbyname";
			public const string ModifiedByYomiName = "modifiedbyyominame";
			public const string ModifiedOn = "modifiedon";
			public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
			public const string ModifiedOnBehalfByName = "modifiedonbehalfbyname";
			public const string ModifiedOnBehalfByYomiName = "modifiedonbehalfbyyominame";
			public const string MwO_PPactionConfiguration_MsPp_WebRole = "mwo_PPActionConfiguration_mspp_WebRole";
			public const string Name = "name";
			public const string OverriddenCreatedOn = "overriddencreatedon";
			public const string OverwriteTime = "overwritetime";
			public const string OwnerId = "ownerid";
			public const string OwnerIdName = "owneridname";
			public const string OwnerIdYomiName = "owneridyominame";
			public const string OwningBusinessUnit = "owningbusinessunit";
			public const string OwningBusinessUnitName = "owningbusinessunitname";
			public const string OwningTeam = "owningteam";
			public const string OwningUser = "owninguser";
			public const string PowerPageComponent_MsPp_WebRole_Contact = "powerpagecomponent_mspp_webrole_contact";
			public const string PowerPageComponent_PowerPageComponent = "powerpagecomponent_powerpagecomponent";
			public const string PowerPageComponentId = "powerpagecomponentid";
			public const string Id = "powerpagecomponentid";
			public const string PowerPageComponentType = "powerpagecomponenttype";
			public const string PowerPageComponentTypeName = "powerpagecomponenttypename";
			public const string PowerPageSiteId = "powerpagesiteid";
			public const string PowerPageSiteIdName = "powerpagesiteidname";
			public const string PowerPageSiteLanguageId = "powerpagesitelanguageid";
			public const string PowerPageSiteLanguageIdName = "powerpagesitelanguageidname";
			public const string SearchContent = "searchcontent";
			public const string SolutionId = "solutionid";
			public const string StateCode = "statecode";
			public const string StateCodename = "statecodename";
			public const string StatusCode = "statuscode";
			public const string StatusCodename = "statuscodename";
			public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
			public const string UtcConversionTimeZoneCode = "utcconversiontimezonecode";
			public const string VersionNumber = "versionnumber";
		}
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public PowerPageComponent(System.Guid id) : 
				base(EntityLogicalName, id)
		{
		}
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public PowerPageComponent(string keyName, object keyValue) : 
				base(EntityLogicalName, keyName, keyValue)
		{
		}
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public PowerPageComponent(Microsoft.Xrm.Sdk.KeyAttributeCollection keyAttributes) : 
				base(EntityLogicalName, keyAttributes)
		{
		}
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public PowerPageComponent() : 
				base(EntityLogicalName)
		{
		}
		
		public const string PrimaryIdAttribute = "powerpagecomponentid";
		
		public const string PrimaryNameAttribute = "name";
		
		public const string EntitySchemaName = "powerpagecomponent";
		
		public const string EntityLogicalName = "powerpagecomponent";
		
		public const string EntityLogicalCollectionName = "powerpagecomponents";
		
		public const string EntitySetName = "powerpagecomponents";
		
		/// <summary>
		/// For internal use only.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("componentidunique")]
		public System.Nullable<System.Guid> ComponentIdUnique
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("componentidunique");
			}
		}
		
		/// <summary>
		/// For internal use only.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("componentstate")]
		public virtual ComponentState? ComponentState
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((ComponentState?)(EntityOptionSetEnum.GetEnum(this, "componentstate")));
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("componentstatename")]
		public string ComponentStateName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("componentstate"))
				{
					return this.FormattedValues["componentstate"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("content")]
		public string Content
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("content");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("content", value);
			}
		}
		
		/// <summary>
		/// Unique identifier of the user who created the record.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdby")]
		public Microsoft.Xrm.Sdk.EntityReference CreatedBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("createdby");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdbyname")]
		public string CreatedByName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("createdby"))
				{
					return this.FormattedValues["createdby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdbyyominame")]
		public string CreatedByYomiName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("createdby"))
				{
					return this.FormattedValues["createdby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Date and time when the record was created.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdon")]
		public System.Nullable<System.DateTime> CreatedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("createdon");
			}
		}
		
		/// <summary>
		/// Unique identifier of the delegate user who created the record.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfby")]
		public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("createdonbehalfby");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("createdonbehalfby", value);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfbyname")]
		public string CreatedOnBehalfByName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("createdonbehalfby"))
				{
					return this.FormattedValues["createdonbehalfby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfbyyominame")]
		public string CreatedOnBehalfByYomiName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("createdonbehalfby"))
				{
					return this.FormattedValues["createdonbehalfby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// File Content column contains portal web files e.g. png, css etc.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("filecontent")]
		public object FileContent
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<object>("filecontent");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("filecontent_name")]
		public string FileContent_Name
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("filecontent"))
				{
					return this.FormattedValues["filecontent"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Sequence number of the import that created this record.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("importsequencenumber")]
		public System.Nullable<int> ImportSequenceNumber
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("importsequencenumber");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("importsequencenumber", value);
			}
		}
		
		/// <summary>
		/// For internal use only.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("iscustomizable")]
		public Microsoft.Xrm.Sdk.BooleanManagedProperty IsCustomizable
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.BooleanManagedProperty>("iscustomizable");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("iscustomizable", value);
			}
		}
		
		/// <summary>
		/// Indicates whether the solution component is part of a managed solution.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("ismanaged")]
		public System.Nullable<bool> IsManaged
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<bool>>("ismanaged");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("ismanagedname")]
		public string IsManagedName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("ismanaged"))
				{
					return this.FormattedValues["ismanaged"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Unique identifier of the user who modified the record.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedby")]
		public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("modifiedby");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedbyname")]
		public string ModifiedByName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("modifiedby"))
				{
					return this.FormattedValues["modifiedby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedbyyominame")]
		public string ModifiedByYomiName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("modifiedby"))
				{
					return this.FormattedValues["modifiedby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Date and time when the record was modified.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedon")]
		public System.Nullable<System.DateTime> ModifiedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("modifiedon");
			}
		}
		
		/// <summary>
		/// Unique identifier of the delegate user who modified the record.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfby")]
		public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("modifiedonbehalfby");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("modifiedonbehalfby", value);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfbyname")]
		public string ModifiedOnBehalfByName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("modifiedonbehalfby"))
				{
					return this.FormattedValues["modifiedonbehalfby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfbyyominame")]
		public string ModifiedOnBehalfByYomiName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("modifiedonbehalfby"))
				{
					return this.FormattedValues["modifiedonbehalfby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// The name of the custom entity.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("name")]
		public string Name
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("name");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("name", value);
			}
		}
		
		/// <summary>
		/// Date and time that the record was migrated.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("overriddencreatedon")]
		public System.Nullable<System.DateTime> OverriddenCreatedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("overriddencreatedon");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("overriddencreatedon", value);
			}
		}
		
		/// <summary>
		/// For internal use only.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("overwritetime")]
		public System.Nullable<System.DateTime> OverwriteTime
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("overwritetime");
			}
		}
		
		/// <summary>
		/// Owner Id
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("ownerid")]
		public Microsoft.Xrm.Sdk.EntityReference OwnerId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("ownerid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("ownerid", value);
			}
		}
		
		/// <summary>
		/// Name of the owner
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owneridname")]
		public string OwnerIdName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("ownerid"))
				{
					return this.FormattedValues["ownerid"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Yomi name of the owner
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owneridyominame")]
		public string OwnerIdYomiName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("ownerid"))
				{
					return this.FormattedValues["ownerid"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Unique identifier for the business unit that owns the record
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owningbusinessunit")]
		public Microsoft.Xrm.Sdk.EntityReference OwningBusinessUnit
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("owningbusinessunit");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owningbusinessunitname")]
		public string OwningBusinessUnitName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("owningbusinessunit"))
				{
					return this.FormattedValues["owningbusinessunit"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Unique identifier for the team that owns the record.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owningteam")]
		public Microsoft.Xrm.Sdk.EntityReference OwningTeam
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("owningteam");
			}
		}
		
		/// <summary>
		/// Unique identifier for the user that owns the record.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owninguser")]
		public Microsoft.Xrm.Sdk.EntityReference OwningUser
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("owninguser");
			}
		}
		
		/// <summary>
		/// Unique identifier for entity instances
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("powerpagecomponentid")]
		public System.Nullable<System.Guid> PowerPageComponentId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("powerpagecomponentid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("powerpagecomponentid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("powerpagecomponentid")]
		public override System.Guid Id
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return base.Id;
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.PowerPageComponentId = value;
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("powerpagecomponenttype")]
		public virtual PowerPageComponentType? PowerPageComponentType
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((PowerPageComponentType?)(EntityOptionSetEnum.GetEnum(this, "powerpagecomponenttype")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("powerpagecomponenttype", value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("powerpagecomponenttypename")]
		public string PowerPageComponentTypeName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("powerpagecomponenttype"))
				{
					return this.FormattedValues["powerpagecomponenttype"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("powerpagesiteid")]
		public Microsoft.Xrm.Sdk.EntityReference PowerPageSiteId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("powerpagesiteid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("powerpagesiteid", value);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("powerpagesiteidname")]
		public string PowerPageSiteIdName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("powerpagesiteid"))
				{
					return this.FormattedValues["powerpagesiteid"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("powerpagesitelanguageid")]
		public Microsoft.Xrm.Sdk.EntityReference PowerPageSiteLanguageId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("powerpagesitelanguageid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("powerpagesitelanguageid", value);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("powerpagesitelanguageidname")]
		public string PowerPageSiteLanguageIdName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("powerpagesitelanguageid"))
				{
					return this.FormattedValues["powerpagesitelanguageid"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("searchcontent")]
		public string SearchContent
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("searchcontent");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("searchcontent", value);
			}
		}
		
		/// <summary>
		/// Unique identifier of the associated solution.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("solutionid")]
		public System.Nullable<System.Guid> SolutionId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("solutionid");
			}
		}
		
		/// <summary>
		/// Status of the Power Pages Component
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statecode")]
		public virtual PowerPageComponent_StateCode? StateCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((PowerPageComponent_StateCode?)(EntityOptionSetEnum.GetEnum(this, "statecode")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("statecode", value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statecodename")]
		public string StateCodename
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("statecode"))
				{
					return this.FormattedValues["statecode"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Reason for the status of the Power Pages Component
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statuscode")]
		public virtual PowerPageComponent_StatusCode? StatusCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((PowerPageComponent_StatusCode?)(EntityOptionSetEnum.GetEnum(this, "statuscode")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("statuscode", value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statuscodename")]
		public string StatusCodename
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (this.FormattedValues.Contains("statuscode"))
				{
					return this.FormattedValues["statuscode"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// For internal use only.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("timezoneruleversionnumber")]
		public System.Nullable<int> TimeZoneRuleVersionNumber
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("timezoneruleversionnumber");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("timezoneruleversionnumber", value);
			}
		}
		
		/// <summary>
		/// Time zone code that was in use when the record was created.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("utcconversiontimezonecode")]
		public System.Nullable<int> UtcConversionTimeZoneCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("utcconversiontimezonecode");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetAttributeValue("utcconversiontimezonecode", value);
			}
		}
		
		/// <summary>
		/// Version Number
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("versionnumber")]
		public System.Nullable<long> VersionNumber
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<long>>("versionnumber");
			}
		}
		
		/// <summary>
		/// N:N mwo_PPActionConfiguration_mspp_WebRole
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("mwo_PPActionConfiguration_mspp_WebRole")]
		public System.Collections.Generic.IEnumerable<PowerPagesCustomApiAdapter.EarlyBounds.MwO_PowerPagesActionConfiguration> MwO_PPactionConfiguration_MsPp_WebRole
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<PowerPagesCustomApiAdapter.EarlyBounds.MwO_PowerPagesActionConfiguration>("mwo_PPActionConfiguration_mspp_WebRole", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetRelatedEntities<PowerPagesCustomApiAdapter.EarlyBounds.MwO_PowerPagesActionConfiguration>("mwo_PPActionConfiguration_mspp_WebRole", null, value);
			}
		}
		
		/// <summary>
		/// N:N powerpagecomponent_mspp_webrole_contact
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("powerpagecomponent_mspp_webrole_contact")]
		public System.Collections.Generic.IEnumerable<PowerPagesCustomApiAdapter.EarlyBounds.Contact> PowerPageComponent_MsPp_WebRole_Contact
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<PowerPagesCustomApiAdapter.EarlyBounds.Contact>("powerpagecomponent_mspp_webrole_contact", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetRelatedEntities<PowerPagesCustomApiAdapter.EarlyBounds.Contact>("powerpagecomponent_mspp_webrole_contact", null, value);
			}
		}
		
		/// <summary>
		/// N:N powerpagecomponent_powerpagecomponent
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("powerpagecomponent_powerpagecomponent", Microsoft.Xrm.Sdk.EntityRole.Referencing)]
		public System.Collections.Generic.IEnumerable<PowerPagesCustomApiAdapter.EarlyBounds.PowerPageComponent> ReferencingPowerPageComponent_PowerPageComponent
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<PowerPagesCustomApiAdapter.EarlyBounds.PowerPageComponent>("powerpagecomponent_powerpagecomponent", Microsoft.Xrm.Sdk.EntityRole.Referencing);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetRelatedEntities<PowerPagesCustomApiAdapter.EarlyBounds.PowerPageComponent>("powerpagecomponent_powerpagecomponent", Microsoft.Xrm.Sdk.EntityRole.Referencing, value);
			}
		}
		
		/// <summary>
		/// N:N powerpagecomponent_powerpagecomponent
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("powerpagecomponent_powerpagecomponent", Microsoft.Xrm.Sdk.EntityRole.Referenced)]
		public System.Collections.Generic.IEnumerable<PowerPagesCustomApiAdapter.EarlyBounds.PowerPageComponent> ReferencedPowerPageComponent_PowerPageComponent
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<PowerPagesCustomApiAdapter.EarlyBounds.PowerPageComponent>("powerpagecomponent_powerpagecomponent", Microsoft.Xrm.Sdk.EntityRole.Referenced);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetRelatedEntities<PowerPagesCustomApiAdapter.EarlyBounds.PowerPageComponent>("powerpagecomponent_powerpagecomponent", Microsoft.Xrm.Sdk.EntityRole.Referenced, value);
			}
		}
		
		/// <summary>
		/// Constructor for populating via LINQ queries given a LINQ anonymous type
		/// <param name="anonymousType">LINQ anonymous type.</param>
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public PowerPageComponent(object anonymousType) : 
				this()
		{
            foreach (var p in anonymousType.GetType().GetProperties())
            {
                var value = p.GetValue(anonymousType, null);
                var name = p.Name.ToLower();
            
                if (name.EndsWith("enum") && value.GetType().BaseType == typeof(System.Enum))
                {
                    value = new Microsoft.Xrm.Sdk.OptionSetValue((int) value);
                    name = name.Remove(name.Length - "enum".Length);
                }
            
                switch (name)
                {
                    case "id":
                        base.Id = (System.Guid)value;
                        Attributes["powerpagecomponentid"] = base.Id;
                        break;
                    case "powerpagecomponentid":
                        var id = (System.Nullable<System.Guid>) value;
                        if(id == null){ continue; }
                        base.Id = id.Value;
                        Attributes[name] = base.Id;
                        break;
                    case "formattedvalues":
                        // Add Support for FormattedValues
                        FormattedValues.AddRange((Microsoft.Xrm.Sdk.FormattedValueCollection)value);
                        break;
                    default:
                        Attributes[name] = value;
                        break;
                }
            }
		}
	}
}
#pragma warning restore CS1591
