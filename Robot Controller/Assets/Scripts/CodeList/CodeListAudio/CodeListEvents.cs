using System;

public static class CodeListEvents{
    public static Action OnNextCodeBlock;
    public static Action OnLastCodeBlock;

    // UI

    public static Action OnAddBlock;
    public static Action OnSwitchBlock;
    public static Action OnDeleteBlock;
}