using System;

namespace ReventoApp
{
	public interface IViewAdapter<U>
	{
        U setView(int resource);
	}

}

