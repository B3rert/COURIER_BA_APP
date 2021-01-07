package crc649a6cd165cc89c9c4;


public class SfAutoCompleteRenderer
	extends crc643f46942d9dd1fff9.ViewRenderer_2
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Syncfusion.SfAutoComplete.XForms.Droid.SfAutoCompleteRenderer, Syncfusion.SfAutoComplete.XForms.Android", SfAutoCompleteRenderer.class, __md_methods);
	}


	public SfAutoCompleteRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == SfAutoCompleteRenderer.class)
			mono.android.TypeManager.Activate ("Syncfusion.SfAutoComplete.XForms.Droid.SfAutoCompleteRenderer, Syncfusion.SfAutoComplete.XForms.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public SfAutoCompleteRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == SfAutoCompleteRenderer.class)
			mono.android.TypeManager.Activate ("Syncfusion.SfAutoComplete.XForms.Droid.SfAutoCompleteRenderer, Syncfusion.SfAutoComplete.XForms.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public SfAutoCompleteRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == SfAutoCompleteRenderer.class)
			mono.android.TypeManager.Activate ("Syncfusion.SfAutoComplete.XForms.Droid.SfAutoCompleteRenderer, Syncfusion.SfAutoComplete.XForms.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
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
