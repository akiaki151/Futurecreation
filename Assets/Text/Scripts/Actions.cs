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
                //gc.Sc.SetScene("004");
                break;
            case 1:
                gc.Sc.ActionMove();
                break;
            case 2:
                gc.Sc.SetScene("004");
                break;
            case 3:
                gc.Sc.SetScene("004");
                break;
            case 4:
                gc.Sc.SetScene("004");
                break;
            case 5:
                gc.Sc.SetScene("004");
                break;
            default:
                break;
        }
    }

    public void Updata()
    {

    }
}
