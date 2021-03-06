﻿/*
 * Created by SharpDevelop.
 * User: Peter Brooks http://www.pbrooks.net
 * Date: 03/09/2011
 * Time: 11:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Microsoft.Win32;
using System;


namespace RFIDKeybWedge
{
	/// <summary>
	/// Description of ReaderConfiguration.
	/// </summary>
	public class ReaderConfiguration
	{
		private RegistryKey configStore;
		
		public ReaderConfiguration()
		{
			configStore = Registry.CurrentUser;
			configStore = configStore.OpenSubKey("SOFTWARE", true);
			configStore = configStore.CreateSubKey("RFIDSchemaTools\\KeyWedge");
		}
		
		public Boolean keyExists(string key){
			return configStore.GetValue(key) == null;
		}
		
		public string getString(string key){
			object value = configStore.GetValue(key);
			if(value==null)
			{
				return null;
			}
			return value.ToString();
		}
		
		public void setString(string key, string value){
			configStore.SetValue(key,value);
		}
	}
}