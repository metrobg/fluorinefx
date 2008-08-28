/*
	FluorineFx open source library 
	Copyright (C) 2007 Zoltan Csibi, zoltan@TheSilentGroup.com, FluorineFx.com 
	
	This library is free software; you can redistribute it and/or
	modify it under the terms of the GNU Lesser General Public
	License as published by the Free Software Foundation; either
	version 2.1 of the License, or (at your option) any later version.
	
	This library is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
	Lesser General Public License for more details.
	
	You should have received a copy of the GNU Lesser General Public
	License along with this library; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
*/
using System;
using System.Data;
using System.Collections;
using System.Reflection;
using FluorineFx.Invocation;

namespace FluorineFx
{
	/// <summary>
    /// The DataSetTypeAttribute specifies the types of data in a DataSet. The attribute controls how a DataSet is sent to the Flex client.
	/// </summary>
    /// <example>
    /// [DataSetType("FlexRemoteObjectSample.PersonVO")]<br>
    /// [DataTableType("phones", "phoneNumbers", "FlexRemoteObjectSample.PhoneVO")]<br>
    /// public DataSet GetDataSet()<br>
    /// {<br>
    ///     DataSet dataSet = new DataSet("mydataset");<br>
    ///     DataTable dataTable = dataSet.Tables.Add("phones");<br>
    ///     dataTable.Columns.Add( "number", typeof(string) );<br>
    ///     dataTable.Rows.Add( new object[] {"123456"} );<br>
    ///     dataTable.Rows.Add( new object[] {"456789"} );<br>
    ///     return dataSet;<br>
    /// }<br>
    /// In this sample Flex will receive a PersonVO object with a "phoneNumbers" property having an Arraycollection of PhoneVO objects.
    /// </example>
	public class DataSetTypeAttribute : System.Attribute, IInvocationResultHandler
	{
		string	_remoteClass;
        /// <summary>
        /// Initializes a new instance of the DataSetTypeAttribute class.
        /// </summary>
        /// <param name="remoteClass">The ActionScript3 class name.</param>
		public DataSetTypeAttribute(string remoteClass)
		{
			_remoteClass = remoteClass;
		}

		#region IInvocationResultHandler Members
        /// <summary>
        /// This method supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="invocationManager"></param>
        /// <param name="methodInfo"></param>
        /// <param name="obj"></param>
        /// <param name="arguments"></param>
        /// <param name="result"></param>
		public void HandleResult(IInvocationManager invocationManager, MethodInfo methodInfo, object obj, object[] arguments, object result)
		{
			if( result is DataSet )
			{
				DataSet dataSet = result as DataSet;
				ASObject asoResult = new ASObject(_remoteClass);

				foreach(DictionaryEntry entry in invocationManager.Properties)
				{
					if( entry.Key is DataTable )
					{
						DataTable dataTable = entry.Key as DataTable;
						if( dataSet.Tables.IndexOf(dataTable) != -1 )
						{
							if( !dataTable.ExtendedProperties.ContainsKey("alias") )
								asoResult[dataTable.TableName] = entry.Value;
							else
								asoResult[ dataTable.ExtendedProperties["alias"] as string ] = entry.Value;
						}
					}
				}
				invocationManager.Result = asoResult;
			}
		}

		#endregion
	}
}