namespace MainProject
{
    internal class Program
    {
        /// <summary>
        /// Main 함수 : Main Thread 의 스택에 가장먼저 쌓을 함수
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Thread.CurrentThread.Name = "Main Thread";
            //
            //Thread thread1 = new Thread(SayHi);
            //thread1.IsBackground = true;
            //thread1.Name = "Thread1";
            //thread1.Start();
            //Console.WriteLine("End");

            // Task : .Net 환경에 의해 관리된 Threadpool에서 Thread 를 빌려다가 작업을 할당하는 클래스

            Account account1 = new Account("Luke" , 100000);
            Account account2 = new Account("Yoda" , 50000);

            //Task task1 = Task.Factory.StartNew(() =>
            //{
            //    while (account1.Balance >= 10000)
            //    {
            //        account1.Transfer(account2, 10000);
            //        Thread.Sleep(5000);
            //    }
            //});

            List<Task> tasks = new List<Task>(10);
            CancellationTokenSource cts = new CancellationTokenSource();

            Task waitingUserInput = Task.Factory.StartNew(() =>
            {
                string input = Console.ReadLine();

                if (input.Equals("s"))
                {
                    cts.Cancel();
                    Console.WriteLine("출금 취소됨");
                }
            });

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 100; j++)
                    {
                        account1.Withdraw(1000);
                        Thread.Sleep(10);
                    }
                },
                cts.Token));
            }

            //tasks.Add(task1);
            Task.WaitAny(Task.WhenAll(tasks.ToArray()), waitingUserInput);

            waitingUserInput.Wait();
        }

        static async void Withdraw1Gold(Account account1, Account account2, int amount = 1)
        {
            account1.Withdraw(amount);
            MyTimer(3).Wait();
            //await MyTimer(3);

            // Task.Wait() : Blocking
            // 호출한 쓰레드를 차단하여 아무 작업도 하지못하도록 함. 
            // 호출한 쓰레드는 계속 차단되어있는 상태로, 컨텍스트 스위칭이 일어나지않고 대기만함. 
            // 그로인해 호출한 쓰레드는 추가적인 작업을 수행할 수 없음.

            // await 는 Non-blocking
            // 호출한 쓰레드를 차단하지 않아서 추가 작업을 할 수 있음. 
            // 호출한 쓰레드를 스택에서 해제하는것이 아니고 Awaitable 의 Continuation 으로 예약되어 
            // 호출한 쓰레드에게 제어권을 반환하므로 추가 작업을 할 수 있게됨.
            // UI 응답은 UI 쓰레드에서 처리되어야하는데, 다른 쓰레드풀 쓰레드에서 UI에 응답을 주어야 하는 경우
            // 쓰레드풀 쓰레드에서 UI 쓰레드로의 컨텍스트 스위칭이 일어날수 있음.

            account2.Withdraw(amount);
        }

        static Task MyTimer(int milliseconds)
        {
            return Task.Factory.StartNew(() =>
            {
                DateTime startTime = DateTime.Now;
                TimeSpan delay = TimeSpan.FromSeconds(3);

                while (true)
                {
                    if (DateTime.Now - startTime >= delay)
                    {
                        break;
                    }
                }
            });
        }


        static void SayHi()
        {
            int i = 0;
            for (i = 0; i < 100; i++)
            {
                Thread.Sleep(100);
            }
            Console.WriteLine($"[{Thread.CurrentThread.Name}] says : Hi {i}");
        }
    }
}
