

public class Actions
{
    GameController gc;
    
    public Actions(GameController gc)
    {
        this.gc = gc;
    }

    //行いたい処理などを書いていく
    public void Test()
    {
        gc.Sc.SetScene("004");
    }

}
