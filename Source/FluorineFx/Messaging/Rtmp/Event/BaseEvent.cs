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
using FluorineFx.Messaging.Api.Event;

namespace FluorineFx.Messaging.Rtmp.Event
{
	/// <summary>
	/// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
	/// </summary>
    [CLSCompliant(false)]
	public class BaseEvent : IRtmpEvent
	{
		protected RtmpHeader _header;
		protected object _object;
		protected int _timestamp;
		protected byte _dataType;
		protected EventType _eventType;
		protected IEventListener _source;

        internal BaseEvent(EventType eventType)
		{
			_eventType = eventType;
			_object = null;
		}

        internal BaseEvent(EventType eventType, byte dataType, IEventListener source) 
		{
			_dataType = dataType;
			_eventType = eventType;
			_source = source;
		}

		public EventType EventType
		{ 
			get{ return _eventType; }
			set{ _eventType = value; }
		}

		public RtmpHeader Header
		{
			get{ return _header; }
			set{ _header = value; }
		}

		public virtual object Object
		{ 
			get{ return _object; }
		}

		public int Timestamp
		{
			get{ return _timestamp; }
			set{ _timestamp = value; }
		}

        public byte DataType
		{
			get{ return _dataType; }
			set{ _dataType = value; }
		}

		public IEventListener Source
		{
			get{ return _source; }
			set{ _source = value; }
		}

		public bool HasSource
		{
			get{ return _source != null; }
		}
	}
}