using System.Collections;
using System.Collections.Generic;

public class Command
{
    public int Parent_Card_id = -1;

    //0:Clear Object(need id)
    //1:Draw Card(need value 1)
    public int Effect = -1;

    public int value1 = 0;
}

public class Stack
{

    public void Execute_Stack()
    {
        ;
    }


    public void Stack_Add_Command(Command Input)
    {
        ;
    }
}