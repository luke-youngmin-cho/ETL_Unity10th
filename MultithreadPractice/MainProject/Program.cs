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
            // 호출된 스레드를 차단하여 해당 스레드가 다른 작업을 수행하지 못하도록 합니다.
            // 호출된 스레드는 차단된 상태로 대기하며, 해당 스레드 자체에서는 컨텍스트 스위칭이 일어나지 않습니다. 
            // 그러나 시스템의 다른 스레드에서는 컨텍스트 스위칭이 발생할 수 있습니다.
            // 그로 인해 호출된 스레드는 작업이 완료될 때까지 추가적인 작업을 수행할 수 없습니다.

            // await는 Non-blocking입니다.
            // 호출된 스레드를 차단하지 않기 때문에, 해당 스레드는 추가 작업을 수행할 수 있습니다.
            // await는 호출된 스레드를 해제하지 않고, 비동기 작업이 완료된 후 실행할 남은 코드를 Continuation으로 예약합니다.
            // 호출된 메서드의 실행이 중단되며, 호출된 스레드는 차단되지 않고 다른 작업을 수행할 수 있게 됩니다.
            // UI 응답은 반드시 UI 스레드에서 처리되어야 합니다. 비동기 작업이 다른 스레드풀의 스레드에서 완료된 경우, UI 관련 코드를 실행하기 위해 UI 스레드로의 컨텍스트 스위칭이 일어날 수 있습니다.

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
