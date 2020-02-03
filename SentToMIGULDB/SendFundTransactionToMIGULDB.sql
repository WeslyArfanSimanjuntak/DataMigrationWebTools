DECLARE @Retval VARCHAR(10);
DELETE
FROM [DEV-DB01].[MIGULDB].[dbo].[ps_fund_transaction_history]

INSERT INTO [DEV-DB01].[MIGULDB].[dbo].[ps_fund_transaction_history]
SELECT *
FROM dbo.ps_fund_transaction_history_

BEGIN TRY

	SET @Retval = 'SUCCESS';
END TRY

BEGIN CATCH
	SET @Retval = 'FAILED';
END CATCH

SELECT @Retval
