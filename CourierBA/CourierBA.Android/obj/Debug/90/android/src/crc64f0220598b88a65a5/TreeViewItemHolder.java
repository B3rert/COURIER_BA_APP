package crc64f0220598b88a65a5;


public class TreeViewItemHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Syncfusion.Android.TreeView.TreeViewItemHolder, Syncfusion.SfTreeView.XForms.Android", TreeViewItemHolder.class, __md_methods);
	}


	public TreeViewItemHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == TreeViewItemHolder.class)
			mono.android.TypeManager.Activate ("Syncfusion.Android.TreeView.TreeViewItemHolder, Syncfusion.SfTreeView.XForms.Android", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
