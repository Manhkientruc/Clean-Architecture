# Giới thiệu & Đặt vấn đề

## Tại sao cần Clean Architecture?

Khi phần mềm ngày càng phức tạp, việc quản lý codebase trở nên khó khăn nếu không có cấu trúc rõ ràng. Clean Architecture được giới thiệu như một giải pháp để xử lý các vấn đề thường gặp trong phát triển phần mềm lớn:

### 1. Tránh spaghetti code
Khi project càng lớn, logic nghiệp vụ, truy vấn database và xử lý giao diện thường bị trộn lẫn trong cùng một class. Điều này khiến code trở nên rối rắm, khó đọc và khó bảo trì.

### 2. Dễ bảo trì và mở rộng
Clean Architecture giúp phân tách rõ ràng các phần trong hệ thống. Nhờ đó:
- Khi thêm tính năng hoặc sửa lỗi, chỉ cần thay đổi đúng tầng liên quan.
- Giảm thiểu rủi ro thay đổi một chỗ làm ảnh hưởng đến chỗ khác.

### 3. Tăng khả năng kiểm thử (Testability)
- Logic nghiệp vụ (Use Cases) không phụ thuộc vào database hay UI → dễ kiểm thử độc lập.
- Viết unit test đơn giản hơn, ít cần mock, tốc độ nhanh hơn.

### 4. Giảm phụ thuộc vào công nghệ và framework
- Logic cốt lõi nằm ở tầng riêng biệt, không phụ thuộc vào ASP.NET, Entity Framework, hay bất kỳ công nghệ cụ thể nào.
- Có thể thay đổi UI, database hoặc framework mà không ảnh hưởng đến business logic.

### 5. Tăng độ bền vững cho dự án
- Khi team thay người hoặc thay đổi công nghệ, logic nghiệp vụ vẫn được bảo toàn.
- Hệ thống có thể tồn tại và phát triển lâu dài mà không bị "nát" theo thời gian.

### 6. Hỗ trợ làm việc nhóm hiệu quả hơn
- Dễ phân chia công việc theo tầng: UI, logic nghiệp vụ, data access.
- Giảm xung đột code, dễ quản lý, dễ code review.

---

## Vấn đề của Monolith Spaghetti Code

Hệ thống monolith được xây dựng không có kiến trúc rõ ràng rất dễ rơi vào tình trạng "spaghetti code", với hàng loạt vấn đề đi kèm:

### 1. Không có ranh giới rõ ràng giữa các phần
- Giao diện, logic nghiệp vụ và truy cập dữ liệu nằm lẫn lộn trong cùng một lớp.
- Khi thay đổi một phần, rất khó kiểm soát ảnh hưởng đến các phần còn lại.

### 2. Khó bảo trì và mở rộng
- Dễ phát sinh lỗi khi sửa hoặc thêm chức năng.
- Tốn thời gian hiểu code cũ và đánh giá rủi ro thay đổi.

### 3. Code phụ thuộc lẫn nhau chằng chịt
- Các thành phần gọi qua lại, gây ra phụ thuộc vòng lặp.
- Khó tách module, khó tái sử dụng.

### 4. Không thể kiểm thử độc lập
- Logic phụ thuộc trực tiếp vào tầng dữ liệu hoặc UI → khó viết test đơn vị.
- Phải kiểm thử toàn hệ thống, tốn thời gian và kém ổn định.

### 5. Khó áp dụng DevOps hoặc CI/CD
- Không thể triển khai từng phần riêng biệt.
- Mỗi thay đổi yêu cầu build và deploy lại toàn bộ hệ thống, dễ sinh lỗi.

### 6. Khó mở rộng đội ngũ phát triển
- Nhiều người cùng sửa một codebase rối rắm → xung đột thường xuyên.
- Dev mới mất nhiều thời gian để hiểu code và làm quen với hệ thống.

# Khái niệm Clean Architecture

## Định nghĩa (theo Uncle Bob)

### 1. Định nghĩa

**Clean Architecture** là một mô hình kiến trúc phần mềm được đề xuất bởi **Robert C. Martin** (Uncle Bob), với mục tiêu tổ chức hệ thống sao cho:
- Dễ hiểu  
- Dễ kiểm thử  
- Dễ bảo trì  
- Và có khả năng mở rộng linh hoạt theo thời gian

Clean Architecture nhấn mạnh việc:
- **Tách biệt logic nghiệp vụ (business rules)** ra khỏi các thành phần phụ thuộc như database, UI, framework, hay các thư viện bên ngoài.
- **Tuân thủ nguyên tắc phụ thuộc một chiều**: các tầng bên ngoài có thể phụ thuộc vào tầng bên trong, nhưng tầng bên trong **không bao giờ được phụ thuộc ngược lại**.

---

### 2. Mục tiêu chính

- Tăng tính **độc lập** của logic nghiệp vụ đối với công nghệ, framework và hạ tầng.
- Dễ dàng **kiểm thử, bảo trì và tái sử dụng** các thành phần quan trọng trong hệ thống.
- Hỗ trợ hệ thống **phát triển bền vững và linh hoạt**, có thể thay đổi công nghệ mà không ảnh hưởng đến phần cốt lõi.
- Giảm rủi ro kỹ thuật và tránh việc hệ thống bị “đóng khung” theo công nghệ ban đầu.

---

### 3. Câu nói nổi tiếng của Uncle Bob:

> **“The architecture should tell us about the system, not about the frameworks it uses.”**

Tạm dịch:

> *“Kiến trúc phần mềm nên phản ánh bản chất của hệ thống, chứ không phải framework mà nó sử dụng.”*

---

## Mục tiêu cốt lõi của Clean Architecture

### 1. Tách biệt rõ ràng giữa các tầng trong hệ thống
- Mỗi tầng có một vai trò riêng biệt: từ xử lý logic nghiệp vụ đến giao tiếp với bên ngoài.
- Giúp hệ thống có cấu trúc rõ ràng, dễ đọc, dễ quản lý và dễ nâng cấp.

### 2. Bảo vệ logic nghiệp vụ khỏi công nghệ bên ngoài
- Logic nghiệp vụ không phụ thuộc vào framework, cơ sở dữ liệu hoặc UI.
- Việc thay đổi công nghệ (VD: từ SQL sang NoSQL) không ảnh hưởng đến tầng cốt lõi.

### 3. Dễ dàng kiểm thử (Testability)
- Do mỗi tầng hoạt động độc lập nên có thể kiểm thử từng phần riêng lẻ.
- Việc viết unit test cho logic nghiệp vụ trở nên đơn giản, nhanh và đáng tin cậy.

### 4. Hỗ trợ bảo trì và phát triển lâu dài
- Giảm "nợ kỹ thuật" nhờ cấu trúc rõ ràng, có định hướng.
- Logic nghiệp vụ có thể tiến hóa mà không làm ảnh hưởng đến tầng ngoài.

### 5. Tăng tính tái sử dụng và mở rộng
- Các module có thể dễ dàng tái sử dụng ở nhiều bối cảnh khác nhau.
- Hệ thống dễ dàng mở rộng theo chiều ngang bằng cách tách các phần độc lập.


# Các tầng (Layers) chính

## Entities (Domain Models)

### Định nghĩa

`Entities` là tầng sâu nhất và quan trọng nhất trong Clean Architecture.  
Nó chứa các đối tượng cốt lõi mô tả nghiệp vụ (**business objects**) và các quy tắc nghiệp vụ cấp cao (**enterprise rules**).

### Đặc điểm chính

- Độc lập hoàn toàn với bất kỳ công nghệ nào như framework, database, hay UI.
- Không phụ thuộc vào bất kỳ tầng nào khác trong hệ thống.
- Là nơi lưu trữ logic nghiệp vụ bền vững, có thể tồn tại trong nhiều năm dù công nghệ thay đổi.

### Ví dụ

Giả sử đang xây dựng hệ thống quản lý đơn hàng, thì các entity có thể là: `Order`, `Customer`, `Product`.

Mỗi entity thường bao gồm:
- **Thuộc tính:** `OrderId`, `CustomerId`, `TotalAmount`,...
- **Quy tắc nghiệp vụ:** `Order.CalculateTotal()`, `Order.CanBeCancelled()`...

```csharp
public class Order
{
    public Guid Id { get; private set; }
    public List<OrderItem> Items { get; private set; }
    public decimal TotalAmount => Items.Sum(i => i.Price * i.Quantity);

    public bool CanBeCancelled(DateTime now)
    {
        return (now - CreatedAt).TotalHours < 24;
    }

    public DateTime CreatedAt { get; private set; }
}
```

### Vai trò trong hệ thống

- Là nền tảng ổn định nhất, nơi các tầng khác (Use Cases, Adapters, Frameworks) đều phụ thuộc vào.
- Đảm bảo hệ thống phản ánh đúng các quy tắc nghiệp vụ, không chỉ đơn thuần là thao tác dữ liệu.

## Use Cases / Application Layer
### Định nghĩa

Use Cases (hay còn gọi là Application Layer) là tầng chịu trách nhiệm triển khai các **quy trình nghiệp vụ cụ thể** của hệ thống.  
Tầng này sử dụng các `Entities` để thực hiện nghiệp vụ, nhưng **không biết gì về UI, database hay framework bên ngoài**.

---

### Chức năng chính

- Thực thi các hành động nghiệp vụ như: “Tạo đơn hàng”, “Xử lý thanh toán”, “Gửi email xác nhận”,...
- Đóng vai trò như một "điều phối viên" giữa các tầng: gọi repository, kiểm tra logic nghiệp vụ, xử lý kết quả và trả về tầng ngoài (API/Controller).

---

### Đặc điểm

- Phụ thuộc vào `Entities`, nhưng **không phụ thuộc vào UI hoặc tầng dữ liệu**.
- Chứa logic nghiệp vụ ở cấp ứng dụng, không phải logic nghiệp vụ cốt lõi.
- Các hành động thường được viết thành **service**, **command handler** hoặc **interactor**.

---

### Ví dụ

```csharp
public class CreateOrderUseCase
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Guid> ExecuteAsync(CreateOrderRequest request)
    {
        var order = new Order(request.CustomerId);
        foreach (var item in request.Items)
        {
            order.AddItem(item.ProductId, item.Quantity, item.Price);
        }

        await _orderRepository.AddAsync(order);
        return order.Id;
    }
}

```

### Vai trò trong hệ thống
- Là cầu nối giữa logic nghiệp vụ (Entities) và thế giới bên ngoài (UI, DB, API).
- Giúp hệ thống có thể thay đổi UI hay database mà không ảnh hưởng đến logic xử lý nghiệp vụ.

## Interface Adapters (Controllers, Gateways, Presenters)
### Định nghĩa
Interface Adapters là tầng chuyển đổi dữ liệu và điều phối giao tiếp giữa hệ thống bên trong và bên ngoài.
Nó đóng vai trò “trung gian dịch thuật” giữa các Use Case và công nghệ như Web API, cơ sở dữ liệu, hoặc UI.
### Chức năng chính
- Nhận dữ liệu từ UI/API, chuyển thành dạng mà Use Case có thể xử lý (Input DTO).
- Gọi các Use Case để xử lý logic nghiệp vụ.
- Chuyển kết quả từ Use Case thành dữ liệu phù hợp để trả ra ngoài (ViewModel, JSON…).
- Giao tiếp với database qua interface repository (thường là implementation của IGateway hoặc IRepository).
### Thành phần thường có
- Controllers: nhận request từ phía người dùng (API, Web).
- Gateways (Repositories): triển khai giao tiếp với cơ sở dữ liệu hoặc service bên ngoài.
- Presenters / View Models: chuẩn bị dữ liệu để trả về UI hoặc API response.
### Ví dụ
1. Controllers
```csharp
[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly CreateOrderUseCase _createOrderUseCase;

    public OrdersController(CreateOrderUseCase createOrderUseCase)
    {
        _createOrderUseCase = createOrderUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        var orderId = await _createOrderUseCase.ExecuteAsync(request);
        return Ok(new { Id = orderId });
    }
}
```

2. Repository (Gateway Implementation)
```csharp
public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public async Task AddAsync(Order order)
    {
        var entity = OrderMapper.ToEntity(order);
        _context.Orders.Add(entity);
        await _context.SaveChangesAsync();
    }
}
```

### Vai trò trong hệ thống
- Đảm bảo tầng trong (Application, Entities) không bị phụ thuộc vào các công nghệ bên ngoài như ASP.NET, EF Core, hay các API cụ thể.
- Dễ dàng thay đổi giao diện (UI, Web API) hay thay đổi hạ tầng (database) mà không ảnh hưởng đến logic cốt lõi.

## Frameworks & Drivers (UI, DB, External APIs)
### Định nghĩa
- Frameworks & Drivers là tầng ngoài cùng trong Clean Architecture, nơi chứa các thành phần cụ thể phụ thuộc vào công nghệ, chẳng hạn như:
        Giao diện người dùng (Web UI, Blazor, Razor Pages…)
        Framework backend (ASP.NET Core)
        Database (SQL Server, MongoDB, PostgreSQL…)
        Các dịch vụ bên ngoài (REST API, Message Queue, Email, File Storage, v.v.)

### Đặc điểm chính
- Đây là nơi "đi vào và đi ra" của hệ thống, chịu trách nhiệm giao tiếp với thế giới bên ngoài.
- Phụ thuộc vào các tầng bên trong, không được để tầng bên trong phụ thuộc lại.
- Thường xuyên thay đổi theo yêu cầu công nghệ, nhưng logic cốt lõi không bị ảnh hưởng.

### Chức năng chính
- Giao tiếp và triển khai cụ thể các cổng (gateways) được định nghĩa bởi tầng Application (Use Case).
- Ví dụ:
    ASP.NET Controller nằm ở đây, nhưng phụ thuộc vào interface ở Application.
    Entity Framework DbContext nằm ở đây, triển khai repository interface của tầng trên.
    Giao diện frontend (React, Angular) có thể gọi vào tầng Web API ở tầng này.
### Ví dụ
```csharp
public class SqlOrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public async Task<Order> GetByIdAsync(Guid id)
    {
        var entity = await _context.Orders.FindAsync(id);
        return OrderMapper.ToDomain(entity);
    }
}
```

### Vai trò trong hệ thống
- Là nơi cắm công nghệ vào hệ thống, nhưng theo cách có kiểm soát.
- Cho phép thay thế dễ dàng: từ EF → Dapper, hoặc từ REST → gRPC mà không động đến business logic.

# Nguyên tắc phụ thuộc (Dependency Rule)
## Luồng phụ thuộc từ “ngoài vào trong”
### Định nghĩa
- Dependency Rule là nguyên tắc cốt lõi trong Clean Architecture, quy định rằng các tầng bên ngoài có thể phụ thuộc vào tầng bên trong, nhưng tầng bên trong tuyệt đối không được phụ thuộc ngược lại.

### Luồng phụ thuộc
- Luồng phụ thuộc trong Clean Architecture luôn đi theo hướng:
- Frameworks & Drivers → Interface Adapters → Use Cases → Entities
- Nghĩa là:
    Use Cases có thể gọi Entities.
    Interface Adapters có thể gọi Use Cases.
    Frameworks có thể gọi Controller, Repository, Service ở tầng Adapter.
    Nhưng không bao giờ ngược lại.

### Ý nghĩa
- Entities và Use Cases không được biết gì về web, database, hay giao diện người dùng.
- Giúp hệ thống tách biệt hoàn toàn logic nghiệp vụ với công nghệ cụ thể.
- Khi cần thay đổi công nghệ (ví dụ: từ ASP.NET MVC sang Blazor), bạn chỉ cần sửa các lớp ở tầng ngoài mà không ảnh hưởng đến logic bên trong.

### Ví dụ minh hoạ
- Giả sử có class OrderController ở tầng Web (Frameworks), gọi vào CreateOrderUseCase ở tầng Application:

```csharp
var orderId = await _createOrderUseCase.ExecuteAsync(request);
```

- Ngược lại, bạn không được viết logic trong Use Case kiểu như:

```csharp
var result = _dbContext.Orders.ToList();
```

### Giải pháp để giữ đúng nguyên tắc
- Dùng interface (abstraction) để định nghĩa hành vi cần thiết (ví dụ: IOrderRepository).
- Các tầng trong chỉ gọi qua interface.
- Tầng ngoài chịu trách nhiệm triển khai interface (ví dụ: OrderRepository : IOrderRepository).

## Tầng trong không biết gì về tầng ngoài
- **“Tầng trong không biết gì về tầng ngoài”** – là một trong những điểm mấu chốt làm nên sự “clean” của Clean Architecture.

---

### Nguyên tắc cốt lõi

Trong Clean Architecture, tầng trong (ví dụ: `Entities`, `Use Cases`) không được phép biết bất kỳ điều gì về tầng ngoài như:

 Giao diện người dùng (UI)
 Cơ sở dữ liệu (DB)
 Framework (ASP.NET, Entity Framework, Blazor,…)
 API hoặc dịch vụ bên ngoài (Email, Message Queue,…)

Tầng trong chỉ biết đến những abstraction (giao diện), chứ không quan tâm cách mà tầng ngoài thực thi cụ thể.

### Vì sao lại như vậy?

 Để bảo vệ logic nghiệp vụ – phần cốt lõi của hệ thống – khỏi những thay đổi liên quan đến công nghệ.
 Đảm bảo code ở tầng trong luôn dễ test, dễ đọc và dễ tái sử dụng.
 Tránh tình trạng code business logic bị "dính chặt" với framework hoặc database cụ thể.

### Cách thực hiện

Để tầng trong không phụ thuộc tầng ngoài, Clean Architecture thường dùng:

Dependency Inversion Principle (DIP) – Đảo ngược sự phụ thuộc.
Interface-based Programming – Các tầng trong định nghĩa interface (ví dụ: `IOrderRepository`), tầng ngoài implement nó.

> Tầng trong “ra yêu cầu”, tầng ngoài “đáp ứng”.


### Ví dụ minh hoạ

2. Tầng trong:

```csharp
public interface IOrderRepository
{
    Task AddAsync(Order order);
}
```

1. Tầng ngoài:

```csharp
public class SqlOrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public async Task AddAsync(Order order)
    {
        var entity = OrderMapper.ToEntity(order);
        _context.Orders.Add(entity);
        await _context.SaveChangesAsync();
    }
}
```

Ở đây:
`Use Case` chỉ gọi `IOrderRepository`, không biết gì về AppDbContext hay EF Core.
EF Core được sử dụng chỉ ở tầng ngoài (Frameworks & Drivers).

# Ưu & nhược điểm
## Ưu điểm của Clean Architecture

Clean Architecture mang lại nhiều lợi ích trong việc thiết kế và xây dựng hệ thống phần mềm hiện đại, đặc biệt là các hệ thống quy mô lớn hoặc yêu cầu bảo trì lâu dài.

---

### 1. Dễ bảo trì (Maintainable)

- Kiến trúc phân tầng rõ ràng giúp dễ xác định vị trí cần chỉnh sửa.
- Khi thêm tính năng hoặc sửa lỗi, chỉ cần làm việc trong đúng tầng liên quan, giảm rủi ro ảnh hưởng đến toàn hệ thống.

---

### 2. Dễ kiểm thử (Testable)

- Các tầng bên trong như `Entities` và `Use Cases` không phụ thuộc vào công nghệ bên ngoài → có thể kiểm thử độc lập.
- Dễ viết unit test, ít cần mock, tăng độ tin cậy khi triển khai CI/CD.

---

### 3. Dễ mở rộng (Scalable)

- Dễ dàng thêm tính năng mới hoặc thay đổi logic nghiệp vụ mà không ảnh hưởng đến cấu trúc tổng thể.
- Có thể mở rộng hệ thống theo chiều ngang bằng cách tách các module riêng biệt.

---

### 4. Tái sử dụng cao (Reusable)

- Các thành phần như logic nghiệp vụ hoặc validation có thể tái sử dụng trong nhiều ứng dụng (Web, API, Mobile...).
- Thiết kế theo kiểu interface giúp dễ dàng plug-in hoặc swap module.

---

### 5. Giảm phụ thuộc vào công nghệ (Technology-Agnostic)

- Logic nghiệp vụ không bị gắn chặt với framework, UI hay database cụ thể.
- Dễ dàng thay đổi công nghệ (VD: chuyển từ EF Core sang Dapper, từ REST sang gRPC) mà không ảnh hưởng tới logic cốt lõi.

---

### 6. Hỗ trợ làm việc nhóm hiệu quả

- Nhóm phát triển có thể phân chia công việc theo tầng: UI, Application, Infrastructure.
- Giảm xung đột merge, tăng hiệu quả review code và phát triển song song.

---

## Nhược điểm của Clean Architecture

Mặc dù Clean Architecture mang lại nhiều lợi ích lâu dài, nhưng cũng tồn tại một số nhược điểm, đặc biệt ở giai đoạn khởi đầu của dự án.

---

### 1. Đòi hỏi thời gian học (Learning Curve)

- Với những người mới bắt đầu, việc hiểu và áp dụng đầy đủ mô hình Clean Architecture có thể gây bối rối.
- Cần hiểu rõ các khái niệm như: phân tầng, nguyên tắc phụ thuộc (Dependency Rule), Interface vs Implementation,...

---

### 2. Cấu trúc ban đầu phức tạp (Boilerplate)

- Cần tạo nhiều file, folder, interface chỉ để triển khai một chức năng đơn giản.
- Khối lượng “code khung” lớn hơn so với các mô hình đơn giản như MVC.

---

### 3. Tốn effort cho dự án nhỏ

- Với các ứng dụng nhỏ hoặc prototype, việc áp dụng đầy đủ Clean Architecture có thể gây dư thừa.
- Đôi khi không cần chia quá nhiều tầng, vì chi phí tổ chức cao hơn lợi ích mang lại.

---

### 4. Dễ áp dụng sai nếu không hiểu bản chất

- Một số team chỉ áp dụng về mặt cấu trúc thư mục mà không tuân thủ đúng nguyên tắc phụ thuộc.
- Điều này dẫn đến "giả Clean Architecture" – trông có vẻ sạch, nhưng thực chất vẫn spaghetti code bên trong.

---

# So sánh với các kiến trúc khác

## Clean Architecture vs MVC / Layered Architecture

---

### 1. Mô hình MVC / Layered truyền thống

**Mô tả:**
- Gồm các tầng: `Controller` → `Service` → `Repository` → `Database`.
- Logic nghiệp vụ thường nằm rải rác trong cả Controller, Service và Repository.
- Phụ thuộc giữa các tầng là một chiều từ trên xuống: UI → Business → Data.

**Ưu điểm:**
- Dễ học, phổ biến, phù hợp với ứng dụng đơn giản.
- Tạo nhanh, ít boilerplate, phù hợp với MVP hoặc demo nhỏ.

**Nhược điểm:**
- Logic nghiệp vụ dễ bị phân tán (và rò rỉ) vào tầng Controller hoặc Repository.
- Khó mở rộng khi hệ thống lớn dần.
- Tầng business vẫn phụ thuộc trực tiếp vào tầng dữ liệu và framework cụ thể.

---

### 2. Clean Architecture

**Mô tả:**
- Chia thành 4 tầng rõ ràng: `Entities`, `Use Cases`, `Interface Adapters`, `Frameworks & Drivers`.
- Logic nghiệp vụ được giữ hoàn toàn độc lập với UI, DB hoặc công nghệ ngoài.
- Mọi phụ thuộc đều đi từ ngoài vào trong (theo Dependency Rule).

**Ưu điểm:**
- Logic nghiệp vụ tập trung và có thể tái sử dụng.
- Dễ kiểm thử từng tầng độc lập (unit test friendly).
- Dễ thay đổi UI, DB, hoặc framework mà không ảnh hưởng đến lõi hệ thống.

**Nhược điểm:**
- Phức tạp hơn để bắt đầu, nhất là với ứng dụng nhỏ.
- Cần nhiều file, folder, interface (boilerplate).

---

### 3. Tóm tắt so sánh

| Tiêu chí                   | MVC / Layered               | Clean Architecture                |
|----------------------------|------------------------------|------------------------------------|
| Cấu trúc phân tầng         | Cơ bản (3–4 tầng)            | Rõ ràng (4 tầng tách biệt)         |
| Tính độc lập nghiệp vụ     | Thấp                         | Cao                                |
| Tính mở rộng               | Trung bình                   | Cao                                |
| Dễ kiểm thử                | Khó khi logic phụ thuộc nhiều| Dễ kiểm thử từng phần              |
| Phụ thuộc công nghệ        | Có                           | Hạn chế tối đa                     |
| Dễ bắt đầu với dự án nhỏ   | Dễ                           | Có thể dư thừa                     |
| Áp dụng cho hệ thống lớn   | Bắt đầu khó khăn             | Phù hợp về lâu dài                 |

---

**Kết luận:**  
- Với các hệ thống nhỏ hoặc prototype, MVC có thể là lựa chọn hợp lý do tính đơn giản.
- Với hệ thống lớn, phức tạp hoặc cần bảo trì lâu dài, Clean Architecture là lựa chọn ưu việt hơn vì tính tách biệt và khả năng kiểm soát cao.


## Onion Architecture

---

### 1. Định nghĩa

**Onion Architecture** là một mô hình kiến trúc phần mềm được giới thiệu bởi Jeffrey Palermo.  
Mục tiêu chính là **bảo vệ logic nghiệp vụ** bằng cách đặt nó ở trung tâm của kiến trúc và bao quanh bởi các lớp phụ thuộc.

---

### 2. Cấu trúc lớp (Layers)

- **Core (trung tâm):** Domain Models (Entities), Business Logic.
- **Application Services:** Xử lý các use case, workflow.
- **Interfaces (Adapters):** Giao tiếp với thế giới bên ngoài như UI, DB, API.
- **Infrastructure:** Thư viện, framework, hệ thống bên ngoài (EF, SMTP, HTTP...).

Các lớp được tổ chức giống như các **vòng tròn của củ hành**, với logic nghiệp vụ ở lõi và các lớp phụ thuộc bao quanh.

---

### 3. Nguyên tắc chính

- Các vòng tròn **chỉ phụ thuộc vào các vòng tròn bên trong**.
- **Không có lớp nào trong lõi biết gì về lớp bên ngoài.**
- Giao tiếp thông qua **abstraction (interface)** – được định nghĩa ở trong và triển khai ở ngoài.

---

### 4. So sánh với Clean Architecture

| Tiêu chí                    | Onion Architecture                    | Clean Architecture                      |
|----------------------------|----------------------------------------|-----------------------------------------|
| Kiến trúc hình học          | Các vòng tròn đồng tâm (Onion)         | Các vòng tròn hoặc hình chữ nhật phân tầng |
| Định hướng phụ thuộc        | Ngoài → Trong                         | Ngoài → Trong                           |
| Tách biệt logic nghiệp vụ   | Có                                     | Có                                      |
| Tầng Use Cases riêng biệt   | Không rõ ràng (thường gộp vào Services)| Rõ ràng (Use Case Layer riêng)         |
| Giao tiếp giữa tầng         | Interface/DI                          | Interface/DI                            |
| Tính linh hoạt              | Cao                                   | Rất cao                                 |

---

### 5. Khi nào dùng Onion Architecture?

- Khi muốn **bảo vệ domain logic khỏi thay đổi công nghệ**, nhưng chưa cần kiến trúc quá tách biệt như Clean Architecture.
- Phù hợp với các hệ thống **trung bình đến lớn**, nhưng chưa có yêu cầu chia tầng Use Case chi tiết.

---

## Hexagonal Architecture (Ports & Adapters)

---

### 1. Định nghĩa

**Hexagonal Architecture**, còn gọi là **Ports & Adapters**, được đề xuất bởi Alistair Cockburn.  
Mục tiêu của kiến trúc này là **tách biệt hệ thống lõi (core logic)** với các tác nhân bên ngoài (UI, DB, API...) bằng cách sử dụng các cổng (ports) và bộ chuyển đổi (adapters).

---

### 2. Thành phần chính

- **Application Core:** Chứa logic nghiệp vụ và định nghĩa các `ports` (interfaces).
- **Ports:** Là các interface mà core định nghĩa để giao tiếp với bên ngoài (VD: `IOrderRepository`, `INotificationService`...).
- **Adapters:** Là phần triển khai các `ports`, ví dụ như Web Controllers, Database Repositories, File Readers...
- **Drivers:** Các thành phần gọi vào hệ thống, như HTTP requests, CLI, scheduled jobs,...

---

### 3. Nguyên tắc

- **Core logic không biết gì về cách hệ thống được sử dụng** (qua web, mobile hay CLI).
- Mọi tương tác đều đi qua `ports`, được inject bằng `adapters`.
- Hệ thống có thể được test dễ dàng bằng cách thay thế adapter thật bằng adapter giả (mock/fake).

---

### 4. So sánh với Clean Architecture

| Tiêu chí                          | Hexagonal Architecture                 | Clean Architecture              |
|-----------------------------------|----------------------------------------|---------------------------------|
| Khái niệm trung tâm               | Ports (interface) & Adapters           | Entities, Use Cases, Adapters   |
| Luồng phụ thuộc                   | Ngoài → Trong                          | Ngoài → Trong                   |
| Use Cases riêng biệt              | Không phân tầng rõ                     | Có tầng Use Cases riêng         |
| Kiến trúc hình học                | Hình lục giác, không tập trung vào lớp | Vòng tròn/lớp rõ ràng hơn       |
| Phù hợp với dịch vụ hướng sự kiện | Tốt (dễ tích hợp MQ, CLI, webhook)     | Tốt                             |

---

### 5. Khi nào dùng Hexagonal Architecture?

- Khi cần xây dựng hệ thống **hướng cổng vào (entry points)** linh hoạt: HTTP, CLI, Message Queue,...
- Khi muốn hệ thống dễ tích hợp với nhiều loại input/output khác nhau (modular).
- Khi chú trọng **testing**: mock được tất cả adapter bên ngoài một cách dễ dàng.

---

# Áp dụng trong .NET

## Cấu trúc thư mục & project solution

Khi áp dụng Clean Architecture trong .NET, chúng ta thường tổ chức solution thành **nhiều project riêng biệt**, mỗi project đại diện cho một tầng kiến trúc.  
Điều này giúp **tách biệt rõ ràng các phần trong hệ thống**, và đảm bảo nguyên tắc phụ thuộc (Dependency Rule) được thực thi đúng.

---

### 1. Cấu trúc project tổng thể

```text
/src
├── MyApp.Domain         → Entities (Domain models, business rules)
├── MyApp.Application    → Use Cases (Application services, interfaces)
├── MyApp.Infrastructure → Implementation (EF Core, EmailService, etc.)
└── MyApp.WebAPI         → Web layer (Controllers, API endpoints)
```

> Mỗi project là một tầng riêng biệt, chỉ được phép phụ thuộc vào các tầng bên trong nó.

---

### 2. Mô tả vai trò từng project

| Project                | Vai trò chính                                                                 |
|------------------------|--------------------------------------------------------------------------------|
| `MyApp.Domain`         | Chứa các Entity, Enum, Value Object, Domain Event,…                           |
| `MyApp.Application`    | Chứa các Use Cases, DTO, interface repository/service,…                       |
| `MyApp.Infrastructure`| Triển khai các interface từ Application (Repository, EmailService, Cache...) |
| `MyApp.WebAPI`         | Web layer – nơi đặt Controller, Middleware, API endpoint,…                    |

---

### 3. Dependency flow (phụ thuộc đúng hướng)
WebAPI → Application → Domain
↘
Infrastructure

- `WebAPI` gọi `Application` qua interface.
- `Infrastructure` triển khai interface từ `Application`, nhưng **không ngược lại**.
- `Domain` không phụ thuộc vào bất kỳ tầng nào.

---

### 4. Các nguyên tắc khi tổ chức project

- **Không được để Application phụ thuộc vào Infrastructure.**
- Interface (port) nên đặt ở `Application`, còn implementation (adapter) nằm ở `Infrastructure`.
- Dùng **Dependency Injection** trong WebAPI để inject các service từ tầng ngoài vào tầng trong.
- Hạn chế đưa code xử lý logic nghiệp vụ vào Controller hoặc lớp service ở WebAPI.

---

## Ví dụ: Interface, DTO, Service và Repository

Khi áp dụng Clean Architecture, mỗi tầng sẽ giữ một phần trách nhiệm riêng. Dưới đây là ví dụ cụ thể minh hoạ cách tổ chức các phần như `interface`, `DTO`, `service`, `repository` đúng chuẩn.

---

### 1. Interface (Application Layer)

Ở tầng Application, ta định nghĩa các interface (port) để giao tiếp với tầng hạ tầng (infrastructure), ví dụ:

```csharp
// Application/Interfaces/IOrderRepository.cs
public interface IOrderRepository
{
    Task<Order> GetByIdAsync(Guid id);
    Task AddAsync(Order order);
}
```

### 2. DTO (Data Transfer Object)
DTO dùng để truyền dữ liệu từ tầng ngoài vào tầng Application (thường được mapping thành entity bên trong):
```csharp
// Application/DTOs/CreateOrderRequest.cs
public class CreateOrderRequest
{
    public Guid CustomerId { get; set; }
    public List<OrderItemDto> Items { get; set; }
}

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
```
### 3. Service / Use Case Handler (Application Layer)
Một Use Case có thể viết thành một service riêng để xử lý luồng nghiệp vụ:
```csharp
// Application/UseCases/CreateOrderUseCase.cs
public class CreateOrderUseCase
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Guid> ExecuteAsync(CreateOrderRequest request)
    {
        var order = new Order(request.CustomerId);
        foreach (var item in request.Items)
        {
            order.AddItem(item.ProductId, item.Quantity, item.Price);
        }

        await _orderRepository.AddAsync(order);
        return order.Id;
    }
}
```
### 4. Repository Implementation (Infrastructure Layer)
Tầng hạ tầng sẽ triển khai interface từ Application Layer:
```csharp
// Infrastructure/Repositories/OrderRepository.cs
public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public async Task<Order> GetByIdAsync(Guid id)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }
}
```

### 5. Kết nối với WebAPI
Controller sẽ gọi vào Use Case đã inject qua DI container:
```csharp
// WebAPI/Controllers/OrdersController.cs
[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly CreateOrderUseCase _createOrderUseCase;

    public OrdersController(CreateOrderUseCase createOrderUseCase)
    {
        _createOrderUseCase = createOrderUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        var orderId = await _createOrderUseCase.ExecuteAsync(request);
        return Ok(new { Id = orderId });
    }
}
```

## Thư viện & Template Clean Architecture nổi bật trong .NET

Để giúp khởi tạo dự án nhanh chóng và đúng cấu trúc Clean Architecture, cộng đồng .NET đã phát triển nhiều **template uy tín, được sử dụng rộng rãi**. Dưới đây là hai cái tên tiêu biểu:

---

### 1. Jason Taylor - Clean Architecture Template

- **Link GitHub:** [github.com/jasontaylordev/CleanArchitecture](https://github.com/jasontaylordev/CleanArchitecture)
- **Mô tả:** Đây là template Clean Architecture phổ biến nhất trong cộng đồng .NET, được phát triển bởi Jason Taylor (Microsoft MVP).
- **Tính năng nổi bật:**
  - Tổ chức solution theo đúng mô hình 4 tầng: `Domain`, `Application`, `Infrastructure`, `WebUI`.
  - Có sẵn cấu hình DI, validation, MediatR, CQRS, logging,...
  - Tích hợp sẵn unit test và integration test.
  - Hỗ trợ dùng với WebAPI, Blazor hoặc Razor Pages.
- **Cài đặt nhanh:**

```bash
dotnet new --install Clean.Architecture.Solution.Template
dotnet new ca-sln -n MyProject
```
### 2. Ardalis - Clean Architecture Template

- **Link GitHub:** [github.com/ardalis/CleanArchitecture](https://github.com/ardalis/CleanArchitecture)
- **Mô tả:** Được phát triển bởi Steve Smith (aka Ardalis) – một chuyên gia nổi tiếng trong cộng đồng .NET và Domain-Driven Design (DDD).
- **Tính năng nổi bật:**
  - Áp dụng Clean Architecture kết hợp với DDD và SOLID principles.
  - Sử dụng kiểu `AggregateRoot`, `DomainEvent`, `Specification Pattern`, v.v.
  - Tách rõ Application logic và Infrastructure.
  - Có tích hợp mẫu repository theo interface chuẩn.
- **Phù hợp với:** các dự án áp dụng DDD sâu hơn, tổ chức domain mạnh mẽ hơn template của Jason Taylor.

---

### 3. Các thư viện hỗ trợ Clean Architecture thường dùng

| Thư viện                 | Công dụng chính                                               |
|--------------------------|---------------------------------------------------------------|
| `MediatR`                | Triển khai CQRS / gửi request giữa tầng                      |
| `FluentValidation`       | Validation theo cách riêng biệt, tách khỏi model             |
| `AutoMapper`             | Mapping giữa DTO và Entity dễ dàng                           |
| `Ardalis.Specification`  | Query theo pattern Specification                              |
| `Serilog` / `NLog`       | Logging linh hoạt                                             |

# Best Practices & Tips

## Organization: Tên project & Naming Convention

### Tên project theo tầng

Đặt tên rõ ràng để phân biệt các tầng trong Clean Architecture. Một số gợi ý đặt tên:

| Layer               | Tên project gợi ý                       |
|---------------------|------------------------------------------|
| Entities            | `MyApp.Domain` hoặc `MyApp.Core`        |
| Use Cases           | `MyApp.Application`                     |
| Interface Adapters  | `MyApp.WebApi`, `MyApp.Presentation`    |
| Infrastructure      | `MyApp.Infrastructure`                  |
| Shared Utilities    | `MyApp.Shared`, `MyApp.Common`          |

### Quy tắc đặt tên (naming convention)

- **Class**: PascalCase (ví dụ: `CreateOrderUseCase`, `OrderService`)
- **Interface**: Thêm `I` phía trước (ví dụ: `IOrderRepository`, `ILogger`)
- **Method**: PascalCase (ví dụ: `ExecuteAsync`, `CanBeCancelled`)
- **Biến**: camelCase (ví dụ: `orderRepository`, `createdAt`)
- **Thư mục**: PascalCase (ví dụ: `Controllers`, `UseCases`, `Entities`)
- **Tên file**: Trùng tên class, interface chứa trong đó.

### Tips nhỏ:
- Tránh tên project kiểu `Project1.Application` – đổi thành tên thực tế của app để dễ maintain.
- Có thể group chung `Domain` và `Application` nếu dự án nhỏ.
- Không nhét tất cả vào 1 project duy nhất — tách tầng giúp code dễ đọc, dễ test, dễ quản lý.

## Cách test từng layer

Việc tách biệt các tầng trong Clean Architecture giúp việc kiểm thử trở nên dễ dàng, rõ ràng và có thể thực hiện theo từng cấp độ như sau:

### 1. Test tầng Entities (Domain Models)

**Mục tiêu:** Kiểm tra các quy tắc nghiệp vụ cốt lõi.

**Loại test:** Unit Test

**Cách test:**
- Không cần mock bất kỳ thứ gì.
- Test các phương thức trong entity (như `CanBeCancelled()`, `CalculateTotal()`...).

**Ví dụ:**
```csharp
[Fact]
public void Order_CanBeCancelled_IfWithin24Hours_ReturnsTrue()
{
    var order = new Order(DateTime.UtcNow.AddHours(-5));
    Assert.True(order.CanBeCancelled(DateTime.UtcNow));
}
```
### 2. Use Cases / Application Layer

**Mục tiêu**: Kiểm tra luồng xử lý nghiệp vụ cấp ứng dụng (orchestration logic).

- **Loại test**: Unit Test
- **Cách test**: Dùng mock repository, service… để isolate logic Use Case.
- **Kiểm tra thêm**: 
  - Đầu ra (return)
  - Exception (nếu rule bị vi phạm)
  - Validate logic nghiệp vụ theo từng case

---

### 3. Interface Adapters (Controller, Presenter…)

**Mục tiêu**: Kiểm tra điều phối giữa request từ UI → gọi Use Case → response đúng.

- **Loại test**: Unit Test hoặc Integration Test
- **Cách test**:
  - Mock `UseCase` khi test `Controller`
  - Test response trả ra, status code, định dạng JSON...
- **Tips**:
  - Có thể test thêm các `Presenter` hoặc `ViewModel` nếu sử dụng pattern đó

---

### 4. Infrastructure (Frameworks & Drivers)

**Mục tiêu**: Kiểm tra các thành phần phụ thuộc bên ngoài như database, file, HTTP client...

- **Loại test**: Integration Test
- **Cách test**:
  - Dùng DB test (SQLite in-memory, Testcontainers…)
  - Dùng các endpoint thật nếu cần
- **Gợi ý test tools**:
  - `WebApplicationFactory`: spin up ASP.NET Core cho integration test
  - `Testcontainers-dotnet`: chạy PostgreSQL, MongoDB thật trong container khi test

---

### Gợi ý thư viện test trong .NET

| Mục đích            | Thư viện phổ biến                               |
|---------------------|-------------------------------------------------|
| Unit Testing        | `xUnit`, `NUnit`, `MSTest`                      |
| Mocking             | `Moq`, `NSubstitute`, `FakeItEasy`             |
| In-memory DB        | `SQLite`, `EF InMemory`                        |
| Integration Testing | `WebApplicationFactory`, `Testcontainers-dotnet` |

## Dependency Injection trong ASP.NET Core

### Tổng quan

ASP.NET Core tích hợp sẵn cơ chế Dependency Injection (DI) giúp quản lý lifecycle của các đối tượng, tách biệt giữa lớp triển khai và lớp sử dụng. Điều này đặc biệt hữu ích trong kiến trúc Clean Architecture khi mỗi tầng phụ thuộc qua interface.

---

### Đăng ký DI trong `Program.cs`

Các tầng như Use Case, Repository, Service,… được đăng ký qua interface tương ứng.

```csharp
builder.Services.AddScoped<IOrderRepository, SqlOrderRepository>();
builder.Services.AddScoped<CreateOrderUseCase>();
```
### Cách sử dụng trong Controller
```csharp
[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly CreateOrderUseCase _useCase;

    public OrdersController(CreateOrderUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        var id = await _useCase.ExecuteAsync(request);
        return Ok(new { Id = id });
    }
}
```
### Gợi ý tổ chức cấu hình DI theo tầng
- Application Layer: Đăng ký các UseCase / Service
- Infrastructure Layer: Đăng ký các Repository, External Services
- Cross-cutting: Logging, Email, FileStorage, etc.
### Ví dụ file cấu hình DI cho tầng Infrastructure
```csharp
public static class InfrastructureDI
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, SqlOrderRepository>();
        services.AddDbContext<AppDbContext>(...);
        return services;
    }
}
```
Sau đó gọi trong `Program.cs`:

```csharp
builder.Services.AddInfrastructure();
```
# Các ví dụ minh hoạ

## Code snippet minh hoạ flow từ Controller → Use Case → Entity

```csharp
// 1. Controller nhận request từ client
[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly CreateOrderUseCase _useCase;

    public OrdersController(CreateOrderUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        var orderId = await _useCase.ExecuteAsync(request);
        return Ok(new { Id = orderId });
    }
}
// 2. Use Case xử lý logic nghiệp vụ cấp ứng dụng
public class CreateOrderUseCase
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Guid> ExecuteAsync(CreateOrderRequest request)
    {
        var order = new Order(request.CustomerId);
        foreach (var item in request.Items)
        {
            order.AddItem(item.ProductId, item.Quantity, item.Price);
        }

        await _orderRepository.AddAsync(order);
        return order.Id;
    }
}
// 3. Entity chứa logic nghiệp vụ cốt lõi
public class Order
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid CustomerId { get; private set; }
    public List<OrderItem> Items { get; private set; } = new();
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public Order(Guid customerId)
    {
        CustomerId = customerId;
    }

    public void AddItem(Guid productId, int quantity, decimal price)
    {
        Items.Add(new OrderItem(productId, quantity, price));
    }

    public decimal TotalAmount => Items.Sum(i => i.Price * i.Quantity);
}

```

## Sơ đồ minh hoạ (Sequence Diagram)
```csharp
sequenceDiagram
    participant Client
    participant Controller
    participant UseCase
    participant Entity
    participant Repository

    Client->>Controller: POST /api/orders
    Controller->>UseCase: ExecuteAsync(request)
    UseCase->>Entity: new Order(...), AddItem(...)
    UseCase->>Repository: AddAsync(order)
    Repository-->>UseCase: Success
    UseCase-->>Controller: return OrderId
    Controller-->>Client: 200 OK (OrderId)
```
## Class Diagram (simplified)
```csharp
classDiagram
    class OrdersController {
        +CreateOrderUseCase useCase
        +CreateOrder(request)
    }

    class CreateOrderUseCase {
        +IOrderRepository orderRepository
        +ExecuteAsync(request)
    }

    class Order {
        +Guid Id
        +Guid CustomerId
        +List~OrderItem~ Items
        +AddItem(...)
        +TotalAmount
    }

    class IOrderRepository {
        +AddAsync(order)
    }

    OrdersController --> CreateOrderUseCase
    CreateOrderUseCase --> IOrderRepository
    CreateOrderUseCase --> Order
    Order --> OrderItem
```

# Kết luận & Hướng triển khai

## Các bước “bật đèn xanh” cho dự án .NET mới

1. **Phân tích yêu cầu và xác định các Use Case chính**  
   → Tập trung vào nghiệp vụ, tránh phụ thuộc sớm vào công nghệ.

2. **Thiết kế cấu trúc Solution theo Clean Architecture**  
   → Tách rõ các tầng: `Domain`, `Application`, `Infrastructure`, `Web`.

3. **Định nghĩa các Entity và Use Case cốt lõi**  
   → Entity không phụ thuộc framework, chứa logic nghiệp vụ bền vững.  
   → Use Case xử lý orchestration logic, điều phối các bước nghiệp vụ.

4. **Thiết lập Dependency Injection và Repository Pattern**  
   → Sử dụng interface ở tầng trong, triển khai ở tầng ngoài.  
   → Đảm bảo nguyên tắc Dependency Rule.

5. **Viết test cho từng tầng ngay từ đầu**  
   → Unit test cho UseCase, Entity.  
   → Integration test cho Infrastructure, Controller.

6. **Dùng template uy tín (Jason Taylor, Ardalis)**  
   → Khởi tạo project chuẩn, tiết kiệm thời gian setup ban đầu.

---

## Lộ trình tiếp theo: refactor, mở rộng, tích hợp AI

 **Giai đoạn 1 – Ổn định kiến trúc**
- Review codebase theo Clean Architecture.
- Refactor các module logic đang bị chồng chéo.
- Viết test để bảo vệ business logic.

 **Giai đoạn 2 – Mở rộng & tích hợp**
- Mở rộng thêm các tính năng theo Use Case.
- Tách nhỏ các service nếu cần scale.
- Bắt đầu áp dụng Microservice (nếu cần thiết).

 **Giai đoạn 3 – Tích hợp AI / dịch vụ thông minh**
- Tích hợp AI (OpenAI, Azure Cognitive...) ở tầng Application hoặc Infrastructure.
- Xử lý thông minh như: gợi ý, phân tích dữ liệu, NLP, chatbot...

---


