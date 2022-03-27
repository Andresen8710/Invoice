USE InvoiceBDApp

/*Obtener la lista de precios de todos los productos*/

SELECT Id,Name,Description,Price,Stock FROM dbo.Products


   /*Obtener la lista de productos cuya existencia en el inventario haya llegado al mínimo permitido (5 unidades)*/
SELECT Name,description,Price,Stock FROM dbo.Products WITH (NOLOCK) 
where stock = 5

/*Obtener una lista de clientes no mayores de 35 años que hayan realizado compras entre el
1 de febrero de 2000 y el 25 de mayo de 2000*/

SELECT c.FirstName,c.LastName,c.Age,I.Id as InvoiceId, I.Date as PurchaseDate
FROM dbo.Invoices I WITH (NOLOCK) 
INNER JOIN dbo.Customers C WITH (NOLOCK) 
on I.CustomerId = c.Id
where c.Age <= 35
and I.Date between '2000-02-01' and '2000-05-25' 

/*Obtener el valor total vendido por cada producto en el año 2000*/

SELECT P.Id,P.Name, SUM(ID.SubTotal) AS 'SubTotal', SUM(ID.Total) AS 'Total - Tax included'
FROM dbo.InvoiceDetails ID WITH (NOLOCK) 
INNER JOIN dbo.Products P WITH (NOLOCK)
ON ID.ProductId = P.Id
INNER JOIN dbo.Invoices I WITH (NOLOCK) 
ON ID.InvoiceId = I.Id
WHERE YEAR(I.Date) = 2000
GROUP BY P.Id,P.Name

/*Obtener la última fecha de compra de un cliente y según su frecuencia de compra estimar
en qué fecha podría volver a comprar.*/

SELECT I.CustomerId,c.FirstName,c.LastName,MAX(Date) as LastpurchaseDate, DATEADD(DAY,(DATEDIFF(DAY,MIN(Date),MAX(Date)) / (COUNT(1) - 1)),MAX(Date)) as Nextpurchase
FROM dbo.Invoices I WITH (NOLOCK)  
INNER JOIN dbo.Customers C WITH (NOLOCK) 
ON I.CustomerId = C.Id
GROUP BY CustomerId,c.FirstName,c.LastName
having MIN(Date) <> MAX(Date) 
and CustomerId = 6