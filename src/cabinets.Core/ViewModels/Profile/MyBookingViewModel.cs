﻿using System;
using System.Linq;
using System.Threading.Tasks;
using cabinets.Core.Models;
using cabinets.Core.Repositories;
using cabinets.Core.Services;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.Profile
{
	public class MyBookingViewModel : MvxViewModel<Reservation>
	{
		private Reservation _parameter;
		private Reservation _reservation;
		private User _user;

		public override void Prepare(Reservation parameter)
		{
			_parameter = parameter;
			Date = parameter.Date;
		}

		private readonly IProfileService _profileService;
		private readonly IUserRepository _userRepository;
		private DateTime _date;

		public Reservation Reservation
		{
			get => _reservation;
			private set => SetProperty(ref _reservation, value);
		}

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}

		public DateTime Date
		{
			get => _date;
			set => SetProperty(ref _date, value);
		}

		public MyBookingViewModel(IProfileService profileService, IUserRepository userRepository)
		{
			_profileService = profileService;
			_userRepository = userRepository;
		}

		public override async Task Initialize()
		{
			await base.Initialize();

			Reservation = await _profileService.GetReservationDetail(_parameter.Uuid);
			User = _userRepository.GetAll()
								  .Single();

		}

	}
}
