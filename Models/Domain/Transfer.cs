namespace BankSystem.API.Models.Domain
{
    public class Transfer
    {
        public int TransferId { get; set; } 

        public required string SenderAccountNum { get; set; }        
        public required string RecipientAccounNum { get; set; }       

        public decimal SenderBalanceBeforeTransfer { get; set; }
        public decimal RecipientBalanceBeforeTransfer { get; set; }

        public DateTime TransferDateTime { get; set; }

        public decimal SenderBalanceAfterTransfer { get; set; }
        public decimal RecipientBalanceAfterTransfer { get; set; }

        public int CustomerAccountId { get; set; }
        public required CustomerAccount CustomerAccount { get; set; }
        //public int SenderAccountId { get; set; }
        //public required CustomerAccount SenderAccount { get; set; }

        //public int RecipientAccountId { get; set; }
        //public required CustomerAccount RecipientAccount { get; set; }

    }
}
