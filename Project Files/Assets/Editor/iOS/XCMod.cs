using UnityEngine;
using System.Collections;
using System.IO;

namespace UnityEditor.XCodeEditor 
{
	public class XCMod 
	{
		private Hashtable _datastore;
		private ArrayList _libs;
		
		public string name { get; private set; }
		public string path { get; private set; }
		
		public string group {
			get {
				if(null!=_datastore)
				{
					if(_datastore["group"]!=null){
						Debug.Log("Group: "+(string)_datastore["group"]);
						return (string)_datastore["group"];
					}
					else{
						Debug.Log("Groups is null");
						return null;
					}
				}else{
					Debug.Log("Datastore is null. Most likely the projmod file failed to be parsed");
					return null;
				}
			}
		}
		
		public ArrayList patches {
			get {
				return (ArrayList)_datastore["patches"];
			}
		}
		
		public ArrayList libs {
			get {
				if( _libs == null ) {
					_libs = new ArrayList( ((ArrayList)_datastore["libs"]).Count );
					foreach( string fileRef in (ArrayList)_datastore["libs"] ) {
						_libs.Add( new XCModFile( fileRef ) );
					}
				}
				return _libs;
			}
		}
		
		public ArrayList frameworks {
			get {
				return (ArrayList)_datastore["frameworks"];
			}
		}
		
		public ArrayList headerpaths {
			get {
				return (ArrayList)_datastore["headerpaths"];
			}
		}
		
		public ArrayList files {
			get {
				return (ArrayList)_datastore["files"];
			}
		}
		
		public ArrayList folders {
			get {
				return (ArrayList)_datastore["folders"];
			}
		}
		
		public ArrayList excludes {
			get {
				return (ArrayList)_datastore["excludes"];
			}
		}

		public ArrayList linker_flags {
			get {
				return (ArrayList)_datastore["linker_flags"];
			}
		}
		
		public XCMod( string filename )
		{	
			FileInfo projectFileInfo = new FileInfo( filename );
			if( !projectFileInfo.Exists ) {
				Debug.LogWarning( "File does not exist." );
			}
			
			name = System.IO.Path.GetFileNameWithoutExtension( filename );
			path = System.IO.Path.GetDirectoryName( filename );
			
			string contents = projectFileInfo.OpenText().ReadToEnd();
			_datastore = (Hashtable)XUPorterJSON.MiniJSON.jsonDecode( contents );
		}
	}

	public class XCModFile
	{
		public string filePath { get; private set; }
		public bool isWeak { get; private set; }
		
		public XCModFile( string inputString )
		{
			isWeak = false;
			
			if( inputString.Contains( ":" ) ) {
				string[] parts = inputString.Split( ':' );
				filePath = parts[0];
				isWeak = ( parts[1].CompareTo( "weak" ) == 0 );	
			}
			else {
				filePath = inputString;
			}
		}
	}
}