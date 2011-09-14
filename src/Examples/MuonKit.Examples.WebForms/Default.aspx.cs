using System;
using MuonKit.Examples.Domain.Entities;
using MuonKit.Examples.Domain.Services;
using MuonLab.Commons.DI;

namespace MuonKit.Examples.WebForms
{
	public partial class Default : System.Web.UI.Page
	{
		private readonly IUserService userService;

		// Get an IUserService from the container. Crappy, but this is WebForms!
		public Default() : this(DependencyResolver.Current.GetInstance<IUserService>()) { }

		protected Default(IUserService userService)
		{
			this.userService = userService;
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
			// Attach handlers
			this.btnAdd.Click += btnAdd_Click;

			// Handle initial load
			if(!this.Page.IsPostBack)
				populate();
		}

		/// <summary>
		/// Handle the add button click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAdd_Click(object sender, EventArgs e)
		{
			var user = new User();
			user.Name = this.txtName.Text;

			this.userService.Save(user);

			populate();
		}

		/// <summary>
		/// Populates the page with data
		/// </summary>
		private void populate()
		{
			// Call the All() Method and set the data into the repeater and bind it
			this.rptUsers.DataSource = this.userService.All();
			this.rptUsers.DataBind();
		}
	}
}