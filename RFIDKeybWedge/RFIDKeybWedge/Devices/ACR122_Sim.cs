﻿/*
 * Created by SharpDevelop.
 * Author: Peter Brooks http://www.pbrooks.net
 * Date: 04/09/2011
 * Time: 15:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using GemCard;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace RFIDKeybWedge.Devices
{
	/// <summary>
	/// Description of ACR122.
	/// </summary>
	public class ACR122_Sim : PluginDevice
	{
		struct Key{
			public readonly byte dim1;
			public readonly byte dim2;
			public Key(byte d1, byte d2){
				dim1 = d1;
				dim2 = d2;
			}
		}
		
		private static readonly Dictionary<Key, string> tagTypes 
			= new Dictionary<Key, string>
		{
			{ new Key(0x00, 0x01), "mifare1k"},
			{ new Key(0x00, 0x02), "mifare4k" },
			{ new Key(0x00, 0x03), "mifareUltraLight" },
			{ new Key(0x00, 0x26), "mifareMini" },
			{ new Key(0xF0, 0x04), "topazJewl" },
			{ new Key(0xF0, 0x11), "felica212k" },
			{ new Key(0xF0, 0x12), "felica424k" }
		};
		
		private static readonly Dictionary<byte, string> errorMessages
			= new Dictionary<byte, string>
		{
			{0x00, "No error"},
			{0x01, "Time out, the target has not answered"},
			{0x02, "A CRC error has been detected by the contactless UART"},
			{0x03, "A parity error has been detected by the contactless UART"},
			{0x04, "During a MIFARE anti-collision/select operation, an errorneous bit count has been detected."},
			{0x05, "Framing error during MIFARE operation"},
			{0x06, "An abnormal bit-collision has been detected during bit wise anti-collision at 106 kbs"},
			{0x07, "Communication buffer size insufficient"},
			{0x08, "RF buffer overflow has been detected by the contactless UART (bin BufferOvfl of the register CL_ERROR"},
			{0x0A, "In active communication mode, the RF field ahs not been switched on in time by the counterpart (as defined in NFCIP-1 standard"},
			{0x0B, "RF protocol error"},
			{0x0D, "Temperature error: the internal temperature sensor has detected oveheating, and therefore has automatically switched off the antenna drivers"},
			{0x0E, "Internal buffer overflow"},
			{0x10, "Invalid parameter (range, format, ...)"}
			//{0x12, "DEP Protocol: The chip configured in target mode does not suppor 
			/// </summary>
		
		};
		


		
		//private CardNative iCard;
		private APDUCommand apduCmd;
		private APDUResponse apduResp;
		private bool _connected;
		
		
		public ACR122_Sim()
		{
			_connected = false;
			//iCard = new CardNative();
		}
		
		public string getName()
		{
			return "ARC122U Simulator";
		}
		
		public string[] devices(){
			return new string[]{
				"ACR122U 1"
			};
		}
		
		public DeviceQuery select()
		{
			if(!connected())
			{
				return null;
			}
			ACR122Query_Sim query = new ACR122Query_Sim();
			
			return query;
		}
			
		public bool connect(string device)
		{
			//this.iCard.Connect(device,SHARE.Direct,PROTOCOL.T0orT1);
			_connected = true;
			
			return true;
		}
		public bool disconnect()
		{
			//this.iCard.Disconnect(DISCONNECT.Eject);
			_connected = false;
			return true;
		}
		
		public bool connected(){
			return _connected;
		}
		
		class  ACR122Query_Sim : DeviceQuery {
			//private CardNative iCard;
			private int _numCards;
			private byte[] _tagType;
			private byte _tag;
			private byte[] _uid;
			
			
			public ACR122Query_Sim()
			{
				//this.iCard = iCard;
				_numCards=-1;
				select();
				
			}
			
			public void select()
			{
				//Make the request
				//byte[] d1 = new byte[9] {  0xD4, 0x60, 0x01, 0x01, 0x20, 0x23, 0x11, 0x04, 0x10};
				//APDUCommand a1 = new APDUCommand( 0xFF, 0x00, 0x00, 0x00, d1 ,0x09);
				//APDUResponse r1 = this.iCard.Transmit(a1);
				
				//Retrieve the result
				//APDUCommand a2 = new APDUCommand( 0XFF, 0xC0, 0x00, 0x00, null, r1.SW2);
				//APDUResponse r2 = this.iCard.Transmit(a2);
				
				//int data_len = r2.Data.Length;
				
				//Exceptions: No tags found, tag type not mifare (Schema?) Wrong type of tag
				
				//Retrieve the UID from the data returned
				//byte[] _uid = new byte[4];
				//Array.Copy( r2.Data,data_len-4, _uid, 0, 4);
				
				//Store card information
				_numCards = 1;
				_tagType = new Byte[]{
					0x00,
					0x02
				};
				_tag = 0x00;
				
			}
			
			public bool authenticate(byte[] key, byte block)
			{
				/*
				byte[] card_uid=uid();
				if(card_uid==null)
				{
					return false;
				}
				//Send authentication command
				byte[] d1 = new byte[] { 0xD4, 0x40, 0x01, 0x60, block, key[0], key[1], key[2], key[3], key[4], key[5],
					card_uid[0], card_uid[1], card_uid[2], card_uid[3]
				};
				APDUCommand a1 = new APDUCommand( 0xFF, 0x00, 0x00, 0x00, d1, 0x0F);
				APDUResponse r1 = this.iCard.Transmit(a1);
				
				//Status code 61 05 is valid
				
				
				//Read response
				APDUCommand a2 = new APDUCommand( 0xFF, 0xC0, 0x00, 0x00, null, 0x05);
				APDUResponse r2 = this.iCard.Transmit(a2);
				
				//Status code 90 00 is valid, else error
				*/
				return true;
			}
			
			public byte[] readBlock(byte block)
			{
				/*
				byte[] d1 = new Byte[] { 0xD4, 0x40, 0x01, 0x30, block };
				APDUCommand a1 = new APDUCommand( 0xFF, 0x00, 0x00, 0x00, d1, 0x05);
				APDUResponse r1 = this.iCard.Transmit(a1);
				
				//Status code 61 15
				
				APDUCommand a2 = new APDUCommand( 0xFF, 0xC0, 0x00, 0x00, null, 0x15);
				APDUResponse r2 = this.iCard.Transmit(a2);
				
				//Status Code ??
				
				return r2.Data;
				*/
				return new byte[]{
					0x45,
					0x62,
					0x00,
					0x49
				};
			
			}
			
			public int numCards(){ return _numCards; }
			
			public string tagType(){ return tagTypes[new Key(_tagType[0],_tagType[1])]; }
			
			public byte[] uid(){ return _uid; }
		}
		
		
	}
}

