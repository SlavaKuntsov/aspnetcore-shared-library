using System.ComponentModel;

namespace Domain.Enums;

public enum Role
{
	[Description(nameof(Guest))]
	Guest = 0,
	[Description(nameof(User))]
	User = 1,
	[Description(nameof(Admin))]
	Admin = 2
}