public class Actions
{
    GameController gc;

    public Actions(GameController gc)
    {
        this.gc = gc;
    }

    //行いたい処理などを書いていく
    public void SelectAction(int index)
    {
        switch (index)
        {
            case 0:
                gc.Sc.SetScene("004");
                break;
            case 1:
                gc.Sc.ActionMove();
                break;
            default:
                break;
        }
    }

    public void Updata()
    {

    }
}
