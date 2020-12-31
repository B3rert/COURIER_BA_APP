package crc64031b398dd196bec8;


public class MaterialTreeViewRenderer
	extends crc64031b398dd196bec8.TreeViewRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Syncfusion.XForms.Android.TreeView.MaterialTreeViewRenderer, Syncfusion.SfTreeView.XForms.Android", MaterialTreeViewRenderer.class, __md_methods);
	}


	public MaterialTreeViewRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == MaterialTreeViewRenderer.class)
			mono.android.TypeManager.Activate ("Syncfusion.XForms.Android.TreeView.MaterialTreeViewRenderer, Syncfusion.SfTreeView.XForms.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public MaterialTreeViewRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == MaterialTreeViewRenderer.class)
			mono.android.TypeManager.Activate ("Syncfusion.XForms.Android.TreeView.MaterialTreeViewRenderer, Syncfusion.SfTreeView.XForms.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public MaterialTreeViewRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == MaterialTreeViewRenderer.class)
			mono.android.TypeManager.Activate ("Syncfusion.XForms.Android.TreeView.MaterialTreeViewRenderer, Syncfusion.SfTreeView.XForms.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
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
