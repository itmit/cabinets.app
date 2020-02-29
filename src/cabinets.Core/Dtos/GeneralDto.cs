using System.Collections.Generic;

namespace cabinets.Core.Dtos
{
	/// <summary>
	/// Представляет общую реализацию DTO для api.
	/// </summary>
	public class GeneralDto<T>
	{
		#region Properties
		/// <summary>
		/// Возвращает или устанавливает данные возвращаемые от сервиса.
		/// </summary>
		public T Data
		{
			get;
			set;
		}

		public string Error
		{
			get;
			set;
		}

		public Dictionary<string, string[]> Errors
		{
			get;
			set;
		} = new Dictionary<string, string[]>();

		/// <summary>
		/// Возвращает или устанавливает возвращаемое сообщение сообщение.
		/// </summary>
		public string Message
		{
			get;
			set;
		} = "";

		/// <summary>
		/// Возвращает или устанавливает статус ответа.
		/// </summary>
		public bool Success
		{
			get;
			set;
		} = false;
		#endregion
	}
}
