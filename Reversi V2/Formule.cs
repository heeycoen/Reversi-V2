
namespace Reversi_V2
{
    class Formule
    {
        //
        //deze formule berekent of er ingesloten kan worden binnen de grootte van de array zodat er geen errors voorkomen als er ingesloten moet worden
        //
        public bool formule1(int teller, int x, int y, int i, int breedtestatus, int hoogtestatus, int[,] check)
        {
            if (x + check[0, i] * teller <= breedtestatus - 1 && y + check[1, i] * teller <= hoogtestatus - 1 && y + check[1, i] * teller >= 0 && x + check[0, i] * teller >= 0) { return true; }
            else
            {
                return false;
            }
        }
        //
        //deze formule berekent of er ingesloten kan worden binnen de lengte van teller
        //
        public bool formule2(int teller, int x, int y, int i, int[,] bordstatusform, int beurtstatus, int[,] check)
        {
            for (int j = 1; j <= teller; j++)
            {
                if (bordstatusform[x + check[0, i] * j, y + check[1, i] * j] != beurtstatus && bordstatusform[x + check[0, i] * j, y + check[1, i] * j] != 0) { }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
