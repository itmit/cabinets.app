using System;
using Newtonsoft.Json;

namespace cabinets.Core.Dtos
{
	public class NewsDto
	{
		#region Properties
		public string Body
		{
			get;
			set;
		}

		[JsonProperty("photos")]
		public PictureDto[] DetailPictureSources
		{
			get;
			set;
		}

		public string Head
		{
			get;
			set;
		}

		[JsonProperty("preview_picture")]
		public string PreviewPicture
		{
			get;
			set;
		}

		public Guid Uuid
		{
			get;
			set;
		}
		#endregion
	}
}
