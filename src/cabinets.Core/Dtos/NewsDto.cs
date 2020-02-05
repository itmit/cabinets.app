using System;
using Newtonsoft.Json;

namespace cabinets.Core.Dtos
{
	public class NewsDto
	{
		public Guid Uuid
		{
			get;
			set;
		}

		public string Head
		{
			get;
			set;
		}

		public string Body
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

		[JsonProperty("photos")]
		public PictureDto[] DetailPictureSources
		{
			get;
			set;
		}
	}
}
